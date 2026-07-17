using MyTestMvc.Models.Setup;
using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHREmployeeService: IGenericService<HREmployeeDto>
    {
        Task<List<HREmployeeDto>> GetAllEmployeesAsync();
        Task<HREmployeeDto> GetEmployeeByIdAsync(long employeeId);
        Task<HREmployeeDto> CreateEmployeeAsync(HREmployeeDto employee);
        Task<HREmployeeDto> UpdateEmployeeAsync(HREmployeeDto employee);
        Task<HREmployeeDto> DeleteEmployeeAsync(long employeeId);

        Task<HREmployee?> Authenticate(string username, string password);
    }
}
