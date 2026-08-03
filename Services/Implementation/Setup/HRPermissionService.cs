using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HRPermissionService : GenericService<HRPermission, HRPermissionDto>, IHRPermissionService
    {
        public HRPermissionService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }


        public async Task<List<HRPermissionEmployeeRoleDto>> GetPermissionsLst(string paramFor,string paramType,long idReference)
        {
            var permissions = await _context.Database.SqlQuery<HRPermissionEmployeeRoleDto>($@"EXEC sp_Get_Permission
                @paramFor = {paramFor},
                @paramType = {paramType},
                @paramIdReference = {idReference}")
                .ToListAsync();

            return permissions;
        }

        public async Task CreateRolePermisionLinkAsync(List<HRRolePermissionLinkMirror> entity)
        {
            await GetPermissionsLst("HRPermissionByRole", "UpdateMirrorTable", 0);
            await _context.HRRolePermissionLinkMirror.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateEmployeePermissionLinkAsync(List<HREmployeePermissionLinkMirror> entity)
        {
            await GetPermissionsLst("HRPermissionByEmployee", "UpdateMirrorTable", 0);
            await _context.HREmployeePermissionLinkMirror.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

       

    }
}