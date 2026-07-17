using Web.API.Models.DTOS;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IHREmployeeService _employeeService;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AccountService(
            IHREmployeeService employeeService,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService)
        {
            _employeeService = employeeService;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var employee = await _employeeService.Authenticate(request.Username, request.Password);

            if (employee == null)
                return null;

            var accessToken = _jwtService.GenerateAccessToken(employee);

            var refreshToken = await _refreshTokenService.CreateToken(employee.Id);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
        }

        public async Task<LoginResponseDto?> RefreshToken(string refreshToken)
        {
            var token = await _refreshTokenService.GetToken(refreshToken);

            if (token == null)
                return null;

            if (token.IsRevoked || token.Expires <= DateTime.UtcNow)
                return null;

            var accessToken = _jwtService.GenerateAccessToken(token.Employee);
            var newRefreshToken = await _refreshTokenService.RotateToken(refreshToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            return await _refreshTokenService.RevokeToken(refreshToken);
        }
    }
}