using Web.API.Models.Entities.Setup;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> CreateToken(long employeeId);

        Task<RefreshToken?> GetToken(string token);

        Task<bool> RevokeToken(string token);

        Task<RefreshToken> RotateToken(string token);
    }
}
