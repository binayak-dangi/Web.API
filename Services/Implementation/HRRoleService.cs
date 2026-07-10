using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.Data;
using Web.API.DTOs;
using Web.API.Services.CommonService.Implementation;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class HRRoleService : GenericService<HRRole, HRRoleDto>, IHRRoleService
    {
        public HRRoleService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<List<HRRoleDto>> GetAllRolesAsync()
            => GetAllAsync();

        public Task<HRRoleDto?> GetRoleByIdAsync(long id)
            => GetByIdAsync(id);

        public Task<HRRoleDto> CreateRoleAsync(HRRoleDto dto)
            => CreateAsync(dto);

        public Task<HRRoleDto?> UpdateRoleAsync(HRRoleDto dto)
            => UpdateAsync(dto);

        public Task<HRRoleDto> DeleteRoleAsync(long id)
            => SoftDeleteAsync(id);
    }
}