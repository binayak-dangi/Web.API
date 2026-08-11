using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHREmployeeRepository : IBaseRepository<HREmployeeDto>
    {

        Task<HREmployee?> Authenticate(string username, string password);
        Task<HREmployeeDto> CreateEmployeeAsync(HREmployeeDto employeeDto);
        Task<HREmployeeDto?> ResetPasswordAsync(HREmployeeDto employeeDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
        Task<bool> IsUsernameAvailableAsync(string username);

    }
}