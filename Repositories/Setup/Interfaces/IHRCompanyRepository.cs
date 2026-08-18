using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Common;

namespace Web.API.Repositories.Setup.Interfaces
{
    public interface IHRCompanyRepository: IBaseRepository<HRCompanyDto>
    {
        Task<bool> IsCompanyExist(long? id, HRCompanyDto dto);
    }
}
