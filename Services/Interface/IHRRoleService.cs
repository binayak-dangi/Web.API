using Web.API.DTOs;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHRRoleService: IGenericService<HRRoleDto>
    {
        Task<List<HRRoleDto>> GetAllRolesAsync();
        Task<HRRoleDto> GetRoleByIdAsync(long roleId);
        Task<HRRoleDto> CreateRoleAsync(HRRoleDto role);
        Task<HRRoleDto> UpdateRoleAsync(HRRoleDto role);
        Task<bool> DeleteRoleAsync(long roleId);
    }
}
