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
   

    public class AdmMainHeadingRepository : BaseRepository<AdmMainHeading, AdmMainHeadingDto>, IAdmMainHeadingRepository
    {
        public AdmMainHeadingRepository(AppDbContext context, IMapper mapper, IValidator<AdmMainHeadingDto> validator)
            : base(context, mapper, validator)
        {
        }

        public async Task<bool> IsMainHeadingExist(long? id, AdmMainHeadingDto dto)
        {
            var result= await _context.Adm_MainHeading
                .AnyAsync(x => x.MainHeading == dto.MainHeading && (!id.HasValue || x.Id != id.Value));
            return result;
        }

    }
}