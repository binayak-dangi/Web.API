using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHREmailTemplateRepository: IBaseRepository<HREmailTemplateDto>
    {
        Task<bool> IsEmailTemplateExist(long? id, HREmailTemplateDto dto);
    }
}
