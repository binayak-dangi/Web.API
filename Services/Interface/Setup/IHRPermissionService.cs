using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.Interface.Setup
{
    public interface IHRPermissionService: IGenericService<HRPermissionDto>
    {
        Task<List<HRPermissionEmployeeRoleDto>> GetPermissionsLst(string paramFor, string paramType, long idReference);
        Task CreateRolePermisionLinkAsync(List<HRRolePermissionLinkMirror> entity);
        Task CreateEmployeePermissionLinkAsync(List<HREmployeePermissionLinkMirror> entity);


    }
}
