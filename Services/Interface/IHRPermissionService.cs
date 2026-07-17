using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface
{
    public interface IHRPermissionService: IGenericService<HRPermissionDto>
    {
        Task<List<HRPermissionDto>> GetAllPermissionsAsync();
        Task<HRPermissionDto> GetPermissionByIdAsync(long permissionId);
        Task<HRPermissionDto> CreatePermissionAsync(HRPermissionDto permission);
        Task<HRPermissionDto> UpdatePermissionAsync(HRPermissionDto permission);
        Task<HRPermissionDto> DeletePermissionAsync(long permissionId);
    }
}
