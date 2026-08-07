using System.Security.Claims;
using Web.API.Models.Entities.Setup;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IJwtRepository
    {
        string GenerateAccessToken(HREmployee employee);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
