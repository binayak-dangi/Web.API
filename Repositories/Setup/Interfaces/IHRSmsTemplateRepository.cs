using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHRSmsTemplateRepository: IBaseRepository<HRSmsTemplateDto>
    {
        Task<bool> IsSmsTemplateExist(long? id, HRSmsTemplateDto dto);
    }
}
