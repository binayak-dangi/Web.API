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
        public HREmployeeRepository(AppDbContext context, IMapper mapper, IValidator<HREmployeeDto> validator)
            : base(context, mapper, validator)
        {
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