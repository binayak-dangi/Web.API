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
   

    public class AdmElementRepository : BaseRepository<AdmElement, AdmElementDto>, IAdmElementRepository
    {
        public AdmElementRepository(AppDbContext context, IMapper mapper, IValidator<AdmElementDto> validator)
            : base(context, mapper, validator)
        {
        }

        public async Task<bool> IsElementExist(long? id, AdmElementDto dto)
        {
            var result= await _context.Adm_Element
                .AnyAsync(x => x.ElementHead == dto.ElementHead && (!id.HasValue || x.Id != id.Value));
            return result;
        }

    }
}