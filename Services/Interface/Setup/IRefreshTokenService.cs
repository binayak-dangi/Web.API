using Web.API.Models.Entities.Setup;

namespace Web.API.Services.Interface.Setup
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> CreateToken(long employeeId);

        Task<RefreshToken?> GetToken(string token);

        Task<bool> RevokeToken(string token);

        Task<RefreshToken> RotateToken(string token);
    }
}
