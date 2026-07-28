using System.Security.Claims;
using Web.API.Models.Entities.Setup;

namespace Web.API.Services.Interface.Setup
{
    public interface IJwtService
    {
        string GenerateAccessToken(HREmployee employee);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
