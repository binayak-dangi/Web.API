using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.DTOS;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Common;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HRCorporateTitleRepository : BaseRepository<HRCorporateTitle, HRCorporateTitleDto>, IHRCorporateTitleRepository
    {
        public HRCorporateTitleRepository(AppDbContext context, IMapper mapper, IValidator<HRCorporateTitleDto> validator)
            : base(context, mapper, validator)
        {
        }

        public async Task<bool> IsCorporateTitleExist(long? id, HRCorporateTitleDto dto)
        {
            var result = await _context.HRCorporateTitle
                .AnyAsync(x => x.LevelGrade == dto.LevelGrade && (!id.HasValue || x.Id != id.Value));
            return result;
        }
    }
}