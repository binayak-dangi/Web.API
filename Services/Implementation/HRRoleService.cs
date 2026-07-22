using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        {
            return GetAllAsync();
        }


        public Task<HRRoleDto?> GetRoleByIdAsync(long id)
        {
            return GetByIdAsync(id);
        }
            

        public async Task<HRRoleDto> CreateRoleAsync(HRRoleDto dto)
        {
            bool exists = await _context.HRRole
                .AnyAsync(x => x.RoleName.Trim().ToLower() == dto.RoleName.Trim().ToLower()
                               && !x.IsDeleted);

            if (exists)
                throw new Exception("Role name already exists.");

            return await CreateAsync(dto);
        }

        public async Task<HRRoleDto?> UpdateRoleAsync(HRRoleDto dto)
        {
            bool exists = await _context.HRRole
                .AnyAsync(x => x.Id != dto.Id
                               && x.RoleName.Trim().ToLower() == dto.RoleName.Trim().ToLower()
                               && !x.IsDeleted);

            if (exists)
                throw new Exception("Role name already exists.");

            return await UpdateAsync(dto);
        }

        public Task<HRRoleDto> DeleteRoleAsync(long id)
        {
           return SoftDeleteAsync(id);
        }
           
    }
}