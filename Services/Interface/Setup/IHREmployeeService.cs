
using Web.API.Services.CommonService.Interface;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;

namespace Web.API.Services.Interface.Setup
{
    public interface IHREmployeeService: IGenericService<HREmployeeDto>
    {
        
        Task<HREmployee?> Authenticate(string username, string password);
    }
}
