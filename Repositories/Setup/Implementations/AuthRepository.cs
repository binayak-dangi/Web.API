using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHREmployeeRepository _employeeRepository;
        private readonly IJwtRepository _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenService;
        private readonly AppDbContext _context;

        public AuthRepository(IHREmployeeRepository employeeRepository, IJwtRepository jwtService, IRefreshTokenRepository refreshTokenService, AppDbContext context)
        {
            _employeeRepository = employeeRepository;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _context = context;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var employee = await _employeeRepository.Authenticate(request.Username, request.Password);

            if (employee == null)
                return null;

            // First login or password was reset by admin
            if (employee.isNewlyAdded)
            {
                return new LoginResponseDto
                {
                    AccessToken = "",
                    RefreshToken = "",
                    ExpiresAt = null,
                    RequiresPasswordSetup = true,

                    Employee = new HREmployeeDto
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        MiddleName = employee.MiddleName,
                        LastName = employee.LastName,
                        IdHRRole = employee.IdHRRole,
                        IdHRBranch = employee.IdHRBranch,
                        IdHRCompany = employee.IdHRCompany,
                        Email = employee.Email,
                        Username = employee.Username,
                        isNewlyAdded = employee.isNewlyAdded
                    }
                };
            }

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
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    IdHRRole = employee.IdHRRole,
                    IdHRBranch = employee.IdHRBranch,
                    IdHRCompany = employee.IdHRCompany,
                    Email = employee.Email,
                    Username = employee.Username,
                    isNewlyAdded = employee.isNewlyAdded

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
                    IdHRRole = employee.IdHRRole,
                    IdHRBranch = employee.IdHRBranch,
                    IdHRCompany = employee.IdHRCompany
                }
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            return await _refreshTokenService.RevokeToken(refreshToken);
        }

        public async Task<PasswordSetupResult> CompleteFirstPasswordAsync(CompleteFirstPasswordDto dto)
        {
            var employee = await _context.HREmployee
                .FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (employee == null)
                return PasswordSetupResult.EmployeeNotFound;

            if (!employee.isNewlyAdded)
                return PasswordSetupResult.NotNewlyAdded;

            if (string.IsNullOrEmpty(employee.PasswordHash))
                return PasswordSetupResult.InvalidCurrentPassword;

            if (!BCrypt.Net.BCrypt.Verify(
                    dto.CurrentPassword,
                    employee.PasswordHash))
            {
                return PasswordSetupResult.InvalidCurrentPassword;
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
                return PasswordSetupResult.PasswordMismatch;

            if (BCrypt.Net.BCrypt.Verify(dto.NewPassword,employee.PasswordHash))
            {
                return PasswordSetupResult.NewPasswordSameAsCurrent;
            }

            employee.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            employee.isNewlyAdded = false;

            await _context.SaveChangesAsync();

            return PasswordSetupResult.Success;
        }

        public async Task<bool> ChangePasswordAsync(long employeeId, ChangePasswordDto dto)
        {

            var employee = await _context.HREmployee
                .FirstOrDefaultAsync(x => x.Id == employeeId);

            if (employee == null)
                return false;

            if (string.IsNullOrEmpty(employee.PasswordHash))
                return false;

            bool currentPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.CurrentPassword,
                    employee.PasswordHash);

            if (!currentPasswordValid)
                return false;

            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new Exception(
                    "New password and confirm password do not match.");

            if (dto.CurrentPassword == dto.NewPassword)
                throw new Exception(
                    "New password must be different from your current password.");

            employee.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            employee.isNewlyAdded = false;

            await _context.SaveChangesAsync();

            // Revoke existing refresh tokens
            await _refreshTokenService.RevokeAllTokens(employeeId);

            return true;
        }
    }
}