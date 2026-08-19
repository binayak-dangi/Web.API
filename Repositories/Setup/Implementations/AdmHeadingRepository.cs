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
   

    public class AdmHeadingRepository : BaseRepository<AdmHeading, AdmHeadingDto>, IAdmHeadingRepository
    {
        public AdmHeadingRepository(AppDbContext context, IMapper mapper, IValidator<AdmHeadingDto> validator)
            : base(context, mapper, validator)
        {
        }

        public async Task<bool> IsHeadingExist(long? id, AdmHeadingDto dto)
        {
            var result= await _context.Adm_Heading
                .AnyAsync(x => x.Heading == dto.Heading && (!id.HasValue || x.Id != id.Value));
            return result;
        }

    }
}