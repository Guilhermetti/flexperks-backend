using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Options;
using FlexPerks.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlexPerks.Infrastructure.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _opt;
        public JwtTokenService(IOptions<JwtOptions> options) => _opt = options.Value;

        public string GenerateAccessToken(User user, IEnumerable<Claim>? extraClaims = null)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.UniqueName, user.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email)
            };

            if (extraClaims != null)
                claims.AddRange(extraClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_opt.ExpiresMinutes <= 0 ? 60 : _opt.ExpiresMinutes);

            var token = new JwtSecurityToken(
                issuer: string.IsNullOrWhiteSpace(_opt.Issuer) ? null : _opt.Issuer,
                audience: string.IsNullOrWhiteSpace(_opt.Audience) ? null : _opt.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime GetExpiration() => DateTime.UtcNow.AddMinutes(_opt.ExpiresMinutes <= 0 ? 60 : _opt.ExpiresMinutes);
    }
}