using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HRRoleService : GenericService<HRRole, HRRoleDto>, IHRRoleService
    {
        public HRRoleService(AppDbContext context, IMapper mapper, IValidator<HRRoleDto> validator)
            : base(context, mapper, validator)
        {
        }

    }
}