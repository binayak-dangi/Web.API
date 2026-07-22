using AutoMapper;
using Web.API.Models.DTOS;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IHREmployeeService _employeeService;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IMapper _mapper;

        public AuthService(IHREmployeeService employeeService,IJwtService jwtService,IRefreshTokenService refreshTokenService,IMapper mapper)
        {
            _employeeService = employeeService;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
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
                Employee = _mapper.Map<HREmployeeDto>(employee)
            };
        }

        public async Task<LoginResponseDto?> RefreshToken(string refreshToken)
        {
           
           var token = await _refreshTokenService.GetToken(refreshToken);

            if (token == null || token.IsRevoked || token.Expires <= DateTime.Now)
                return null;

            var accessToken = _jwtService.GenerateAccessToken(token.Employee);
            var newRefreshToken = await _refreshTokenService.RotateToken(refreshToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = newRefreshToken.Expires,
                Employee = _mapper.Map<HREmployeeDto>(token.Employee)
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            return await _refreshTokenService.RevokeToken(refreshToken);
        }
    }
}