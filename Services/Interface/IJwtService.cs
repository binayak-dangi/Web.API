using MyTestMvc.Models.Setup;
using System.Security.Claims;

namespace Web.API.Services.Interface
{
    public interface IJwtService
    {
        string GenerateAccessToken(HREmployee employee);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
