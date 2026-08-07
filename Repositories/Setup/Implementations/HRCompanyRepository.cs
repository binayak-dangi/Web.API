using AutoMapper;
using FluentValidation;
using Web.API.Data;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HRCompanyRepository : BaseRepository<HRCompany, HRCompanyDto>, IHRCompanyRepository
    {
        public HRCompanyRepository(AppDbContext context, IMapper mapper, IValidator<HRCompanyDto> validator)
            : base(context, mapper, validator)
        {
        }

    }
}