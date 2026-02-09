using FlexPerks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<BenefitCategory> BenefitCategories { get; set; } = null!;
        public DbSet<PerksWallet> PerksWallets { get; set; } = null!;
        public DbSet<PerkTransaction> PerkTransactions { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<TimeClockEntry> TimeClockEntries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
