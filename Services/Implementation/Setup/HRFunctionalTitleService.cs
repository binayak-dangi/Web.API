using AutoMapper;
using FluentValidation;
using Web.API.Data;
using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HRFunctionalTitleService : GenericService<HRFunctionalTitle, HRFunctionalTitleDto>, IHRFunctionalTitleService
    {
        public HRFunctionalTitleService(AppDbContext context, IMapper mapper, IValidator<HRFunctionalTitleDto> validator)
            : base(context, mapper, validator)
        {
        }

     
    }
}