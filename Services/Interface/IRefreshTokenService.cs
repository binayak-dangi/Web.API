using Web.API.Models.Entities;

namespace Web.API.Services.Interface
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> CreateToken(long employeeId);

        Task<RefreshToken?> GetToken(string token);

        Task<bool> RevokeToken(string token);

        Task<RefreshToken> RotateToken(string token);
    }
}
