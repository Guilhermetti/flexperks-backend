using FlexPerks.Api.Auth;
using FlexPerks.Application.Handlers.Auth;
using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Options;
using FlexPerks.Infrastructure.Data;
using FlexPerks.Infrastructure.Repositories;
using FlexPerks.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

// 1. Configuration
var configuration = builder.Configuration;
var flags = configuration.GetSection("FeatureFlags");
var useInMemory = flags.GetValue<bool>("UseInMemoryDb");
var disableAuth = flags.GetValue<bool>("DisableAuth");
var seedDemo = flags.GetValue<bool>("SeedDemoData");
var jwtSettings = configuration.GetSection("Jwt");

// 2. Services
// 2.1. Entity Framework DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (useInMemory)
        options.UseInMemoryDatabase("FlexPerksDev");
    else
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

// 2.2. Application & Infrastructure services
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddScoped<LoginHandler>();

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IBenefitCategoryRepository, BenefitCategoryRepository>();
builder.Services.AddScoped<IPerksWalletRepository, PerksWalletRepository>();
builder.Services.AddScoped<IPerkTransactionRepository, PerkTransactionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 2.3. Authentication (JWT)
var jwt = builder.Configuration.GetSection("Jwt");
var key = jwt.GetValue<string>("Key");
var issuer = jwt.GetValue<string>("Issuer");
var audience = jwt.GetValue<string>("Audience");

if (disableAuth)
{
    builder.Services.AddAuthentication("Dev")
        .AddScheme<AuthenticationSchemeOptions, DevAuthHandler>("Dev", options => { });
}
else
{
    builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrWhiteSpace(issuer),
            ValidateAudience = !string.IsNullOrWhiteSpace(audience),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });
}

builder.Services.AddAuthorization();

// 2.4. Controllers, Swagger, CORS
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configura o SwaggerGen com metadados
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FlexPerks API",
        Version = "v1",
        Description = "Documentação dos endpoints da API FlexPerks"
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// 3. Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Ativa o middleware do Swagger e da UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexPerks API V1");
        c.RoutePrefix = string.Empty; // opcional: Swagger UI em https://localhost:<port>/
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (useInMemory && seedDemo)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    DemoSeed.Run(db);
}

app.Run();