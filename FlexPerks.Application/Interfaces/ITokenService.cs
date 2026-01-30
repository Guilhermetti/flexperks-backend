using FlexPerks.Domain.Models;
using System.Security.Claims;

namespace FlexPerks.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IEnumerable<Claim>? extraClaims = null);
        DateTime GetExpiration();
    }
}