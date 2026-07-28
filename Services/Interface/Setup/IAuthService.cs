using Web.API.Models.DTOS.Setup;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

    Task<LoginResponseDto?> RefreshToken(string refreshToken);

    Task<bool> LogoutAsync(string refreshToken);
}