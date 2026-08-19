using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations
{
    public class HREmailTemplateRepository : BaseRepository<HREmailTemplate, HREmailTemplateDto>, IHREmailTemplateRepository
    {
        public HREmailTemplateRepository(AppDbContext context, IMapper mapper, IValidator<HREmailTemplateDto> validator)
            : base(context, mapper, validator)
        {
        }
        public async Task<bool> IsEmailTemplateExist(long? id, HREmailTemplateDto dto)
        {
            var result = await _context.HREmailTemplate
                .AnyAsync(x => x.TemplateName == dto.TemplateName && (!id.HasValue || x.Id != id.Value));
            return result;
        }
    }
}