using Web.API.Models.Common;

namespace Web.API.Repositories.Common
{
    public interface IBaseRepository<TDto>
    {
        Task<PagedResult<TDto>> GetAllAsync(PaginationModel pagination);
        Task<TDto?> GetByIdAsync(long id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto?> UpdateAsync(long id, TDto dto);
        Task<TDto?> SoftDeleteAsyncs(long id);
    }
}
