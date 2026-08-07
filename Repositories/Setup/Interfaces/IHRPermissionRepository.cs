using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHRPermissionRepository: IBaseRepository<HRPermissionDto>
    {
        Task<List<HRPermissionEmployeeRoleDto>> GetPermissionsLst(string paramFor, string paramType, long idReference);
        Task CreateRolePermisionLinkAsync(List<HRRolePermissionLinkMirror> entity);
        Task CreateEmployeePermissionLinkAsync(List<HREmployeePermissionLinkMirror> entity);
    }
}
