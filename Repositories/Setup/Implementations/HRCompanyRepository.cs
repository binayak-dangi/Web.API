using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Common;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HRCompanyRepository : BaseRepository<HRCompany, HRCompanyDto>, IHRCompanyRepository
    {
        public HRCompanyRepository(AppDbContext context, IMapper mapper, IValidator<HRCompanyDto> validator)
            : base(context, mapper, validator)
        {
        }

        
         public async Task<bool> IsCompanyExist(long? id, HRCompanyDto dto)
        {
            var result = await _context.HRCompany
                .AnyAsync(x => x.CompanyName == dto.CompanyName && (!id.HasValue || x.Id != id.Value));
            return result;
        }
    }
}