using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHREmployeeRepository : IBaseRepository<HREmployeeDto>
    {
        Task<bool> IsEmployeeExist(long? id, HREmployeeDto dto);
        Task<HREmployee?> Authenticate(string username, string password);
        Task<HREmployeeDto> CreateEmployeeAsync(HREmployeeDto employeeDto);
        Task<HREmployeeDto?> ResetPasswordAsync(long id);
        Task<bool> IsUsernameAvailableAsync(string username);
        Task<HREmployeeDto?> GetMyProfileAsync(long employeeId);
        Task<HREmployeeDto?> UpdateMyProfileAsync(long employeeId, UpdateMyProfileDto dto);

    }
}