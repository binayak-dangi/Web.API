using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using FluentValidation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HREmployeeRepository : BaseRepository<HREmployee, HREmployeeDto>, IHREmployeeRepository
    {
        private readonly IConfiguration _configuration;
        public HREmployeeRepository(AppDbContext context, IMapper mapper, IValidator<HREmployeeDto> validator, IConfiguration configuration)
            : base(context, mapper, validator)
        {
            _configuration = configuration;
        }


        public async Task<HREmployeeDto> CreateEmployeeAsync(HREmployeeDto employeeDto)
        {
            // Get role
            var role = await _context.HRRole
                .FirstOrDefaultAsync(x => x.Id == employeeDto.IdHRRole);

            if (role == null)
                throw new Exception("Invalid employee role.");

            // Get default password based on role
            string defaultPassword = role.RoleName switch
            {
                "Admin" => _configuration["EmployeePasswords:Admin"] ?? "Admin123",
                "System" => _configuration["EmployeePasswords:System"] ?? "System123",
                "Employee" => _configuration["EmployeePasswords:Employee"] ?? "123456",
                _ => "12345"
            };

            // Map DTO to Entity
            var employee = _mapper.Map<HREmployee>(employeeDto);

            // Hash default password
            employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            // Force password change on first login
            employee.isNewlyAdded = true;

            // Add employee
            await _context.HREmployee.AddAsync(employee);

            // Save
            await _context.SaveChangesAsync();

            // Return DTO
            return _mapper.Map<HREmployeeDto>(employee);
        }

        public async Task<HREmployeeDto?> ResetPasswordAsync(HREmployeeDto employeeDto)
        {
            var employee = await _context.HREmployee
                .Include(x => x.HRRole)
                .FirstOrDefaultAsync(x => x.Id == employeeDto.Id);

            if (employee == null)
                return null;

            string defaultPassword = employee.HRRole.RoleName switch
            {
                "Admin" => _configuration["EmployeePasswords:Admin"] ?? "Admin123",
                "System" => _configuration["EmployeePasswords:System"] ?? "System123",
                "Employee" => _configuration["EmployeePasswords:Employee"] ?? "123456",
                _ => "123456"
            };

            employee.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(defaultPassword);

            // Force password change on next login after an admin reset
            employee.isNewlyAdded = true;

            await _context.SaveChangesAsync();

            return _mapper.Map<HREmployeeDto>(employee);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var employee = await _context.HREmployee
                .FirstOrDefaultAsync(x => x.Username == dto.Username);

            if (employee == null)
                return false;

            if (string.IsNullOrEmpty(employee.PasswordHash))
                return false;

            // Check current password
            bool currentPasswordValid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, employee.PasswordHash);

            if (!currentPasswordValid)
                return false;

            // Check new password confirmation
            if (dto.NewPassword != dto.ConfirmNewPassword)
                throw new Exception("New password and confirm password do not match.");

            // Hash new password
            employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            employee.isNewlyAdded = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsUsernameAvailableAsync(string username)
        {
            username = username.Trim().ToLower();

            return !await _context.HREmployee
                .AnyAsync(x => x.Username.ToLower() == username);
        }

        public async Task<HREmployee?> Authenticate(string username, string password)
        {
            var employee = await _context.HREmployee
                .Include(x => x.HRRole)
                .Include(x => x.HRBranch)
                .Include(x => x.HRCorporateTitle)
                .Include(x => x.HRFunctionalTitle)
                .FirstOrDefaultAsync(x => x.Username == username);

            if (employee == null)
                return null;

            if (string.IsNullOrEmpty(employee.PasswordHash))
                return null; // No password stored

            bool passwordValid;

            // Check BCrypt hash
            if (employee.PasswordHash.StartsWith("$2a$") ||
                employee.PasswordHash.StartsWith("$2b$") ||
                employee.PasswordHash.StartsWith("$2y$"))
            {
                passwordValid = BCrypt.Net.BCrypt.Verify(
                    password,
                    employee.PasswordHash
                );
            }
            else
            {
                // Legacy plain text password
                passwordValid = employee.PasswordHash == password;
            }

            if (!passwordValid)
                return null;

            return employee;
        }
    }
}