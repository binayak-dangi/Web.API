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
    public class HRFunctionalTitleRepository : BaseRepository<HRFunctionalTitle, HRFunctionalTitleDto>, IHRFunctionalTitleRepository
    {
        public HRFunctionalTitleRepository(AppDbContext context, IMapper mapper, IValidator<HRFunctionalTitleDto> validator)
            : base(context, mapper, validator)
        {
        }

        public async Task<bool> IsFunctionalTitleExist(long? id, HRFunctionalTitleDto dto)
        {
            var result = await _context.HRFunctionalTitle
                .AnyAsync(x => x.PositionHead == dto.PositionHead && (!id.HasValue || x.Id != id.Value));
            return result;
        }
    }
}