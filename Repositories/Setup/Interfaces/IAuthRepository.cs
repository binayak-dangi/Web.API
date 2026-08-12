using Web.API.Models.DTOS.Setup;

public interface IAuthRepository
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

    Task<LoginResponseDto?> RefreshToken(string refreshToken);

    Task<bool> LogoutAsync(string refreshToken);

    Task<PasswordSetupResult> CompleteFirstPasswordAsync(CompleteFirstPasswordDto dto);

    Task<bool> ChangePasswordAsync(long employeeId, ChangePasswordDto dto);

}