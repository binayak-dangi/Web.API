using Web.API.Models.DTOS.Setup;

public interface IAuthRepository
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

    Task<LoginResponseDto?> RefreshToken(string refreshToken);

    Task<bool> LogoutAsync(string refreshToken);
}