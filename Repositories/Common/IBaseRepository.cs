namespace Web.API.Repositories.Common
{
    public interface IBaseRepository<TDto>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(long id);
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto?> UpdateAsync(long id, TDto dto);
        Task<TDto?> SoftDeleteAsyncs(long id);
    }
}
