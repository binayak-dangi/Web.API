using Web.API.DTOs;

namespace Web.API.Services.Interface
{
    public interface IHRRoleService
    {
        Task<List<HRRoleDto>> GetAllRolesAsync();
        Task<HRRoleDto> GetRoleByIdAsync(long roleId);
        Task<HRRoleDto> CreateRoleAsync(HRRoleDto role);
        Task<HRRoleDto> UpdateRoleAsync(HRRoleDto role);
        Task<bool> DeleteRoleAsync(long roleId);
    }
}
