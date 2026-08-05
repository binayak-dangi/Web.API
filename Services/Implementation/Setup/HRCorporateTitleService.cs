using AutoMapper;
using Web.API.Data;
using FluentValidation; 
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HRCorporateTitleService : GenericService<HRCorporateTitle, HRCorporateTitleDto>, IHRCorporateTitleService
    {
        public HRCorporateTitleService(AppDbContext context, IMapper mapper, IValidator<HRCorporateTitleDto> validator)
            : base(context, mapper, validator)
        {
        }

        
    }
}