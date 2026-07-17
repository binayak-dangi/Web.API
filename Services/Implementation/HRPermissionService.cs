using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.Data;
using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Implementation;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class HRPermissionService : GenericService<HRPermission, HRPermissionDto>, IHRPermissionService
    {
        public HRPermissionService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<List<HRPermissionDto>> GetAllPermissionsAsync()
            => GetAllAsync();

        public Task<HRPermissionDto?> GetPermissionByIdAsync(long id)
            => GetByIdAsync(id);

        public Task<HRPermissionDto> CreatePermissionAsync(HRPermissionDto dto)
            => CreateAsync(dto);

        public Task<HRPermissionDto?> UpdatePermissionAsync(HRPermissionDto dto)
            => UpdateAsync(dto);

        public Task<HRPermissionDto> DeletePermissionAsync(long id)
            => SoftDeleteAsync(id);
    }
}