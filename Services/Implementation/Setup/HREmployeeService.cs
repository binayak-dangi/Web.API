using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;

using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HREmployeeService : GenericService<HREmployee, HREmployeeDto>, IHREmployeeService
    {
        public HREmployeeService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
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