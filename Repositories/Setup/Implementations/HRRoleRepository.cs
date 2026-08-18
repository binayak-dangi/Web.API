using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HRRoleRepository : BaseRepository<HRRole, HRRoleDto>, IHRRoleRepository
    {
        public HRRoleRepository(AppDbContext context, IMapper mapper, IValidator<HRRoleDto> validator)
            : base(context, mapper, validator)
        {
        }
        public async Task<bool> IsRoleExist(long? id, HRRoleDto dto)
        {
            var result = await _context.HRRole
                .AnyAsync(x => x.RoleName == dto.RoleName && (!id.HasValue || x.Id != id.Value));
            return result;
        }
    }
}