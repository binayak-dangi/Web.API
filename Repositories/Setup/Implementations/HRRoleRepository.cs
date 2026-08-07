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

    }
}