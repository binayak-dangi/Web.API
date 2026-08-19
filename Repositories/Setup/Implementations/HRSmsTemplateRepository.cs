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
    public class HRSmsTemplateRepository : BaseRepository<HRSmsTemplate, HRSmsTemplateDto>, IHRSmsTemplateRepository
    {
        public HRSmsTemplateRepository(AppDbContext context, IMapper mapper, IValidator<HRSmsTemplateDto> validator)
            : base(context, mapper, validator)
        {
        }
        public async Task<bool> IsSmsTemplateExist(long? id, HRSmsTemplateDto dto)
        {
            var result = await _context.HRSmsTemplate
                .AnyAsync(x => x.TemplateName == dto.TemplateName && (!id.HasValue || x.Id != id.Value));
            return result;
        }
    }
}