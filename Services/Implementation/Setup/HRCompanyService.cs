using AutoMapper;
using FluentValidation;
using Web.API.Data;
using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HRCompanyService : GenericService<HRCompany, HRCompanyDto>, IHRCompanyService
    {
        public HRCompanyService(AppDbContext context, IMapper mapper, IValidator<HRCompanyDto> validator)
            : base(context, mapper, validator)
        {
        }

    }
}