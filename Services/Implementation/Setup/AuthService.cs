using AutoMapper;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
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
                Employee =  new HREmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    IdHRRole = employee.IdHRRole,
                    IdHRBranch = employee.IdHRBranch,
                    IdHRCompany = employee.IdHRCompany,
                    Email=employee.Email,
                    Username=employee.Username,
                    
                }
            };
        }

        public async Task<LoginResponseDto?> RefreshToken(string refreshToken)
        {
           
           var token = await _refreshTokenService.GetToken(refreshToken);

            if (token == null || token.IsRevoked || token.Expires <= DateTime.Now)
                return null;

            var employee = token.Employee;
            var accessToken = _jwtService.GenerateAccessToken(token.Employee);
            var newRefreshToken = await _refreshTokenService.RotateToken(refreshToken);

            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresAt = newRefreshToken.Expires,
                Employee = new HREmployeeDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    IdHRRole=employee.IdHRRole,
                    IdHRBranch=employee.IdHRBranch,
                    IdHRCompany=employee.IdHRCompany
                }
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            return await _refreshTokenService.RevokeToken(refreshToken);
        }
    }
}