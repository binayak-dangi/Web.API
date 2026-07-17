using Web.API.Models.DTOS;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IHREmployeeService _employeeService;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IHREmployeeService employeeService,IJwtService jwtService,IRefreshTokenService refreshTokenService)
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
                ExpiresAt = refreshToken.Expires,
                Employee = new HREmployeeDto
                {
                    Id = employee.Id,
                    Username = employee.Username,
                    Email = employee.Email ?? string.Empty
                }
            };
        }

        public async Task<LoginResponseDto?> RefreshToken(string refreshToken)
        {
            var token = await _refreshTokenService.GetToken(refreshToken);

            if (token == null || token.IsRevoked || token.Expires <= DateTime.UtcNow)
                return null;

            var accessToken = _jwtService.GenerateAccessToken(token.Employee);
            var newRefreshToken = await _refreshTokenService.RotateToken(refreshToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = newRefreshToken.Expires,
                Employee = new HREmployeeDto
                {
                    Id = token.Employee.Id,
                    Username = token.Employee.Username,
                    Email = token.Employee.Email ?? string.Empty
                }
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            return await _refreshTokenService.RevokeToken(refreshToken);
        }
    }
}