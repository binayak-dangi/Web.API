using AutoMapper;
using Web.API.Data;
using FluentValidation;
using Web.API.Models.DTOS;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HRCorporateTitleRepository : BaseRepository<HRCorporateTitle, HRCorporateTitleDto>, IHRCorporateTitleRepository
    {
        public HRCorporateTitleRepository(AppDbContext context, IMapper mapper, IValidator<HRCorporateTitleDto> validator)
            : base(context, mapper, validator)
        {
        }

        
    }
}