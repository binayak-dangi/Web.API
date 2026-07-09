namespace Web.API.Services.CommonService.Interface
{
    public interface IGenericService<TEntityDto>
    {
        Task<List<TEntityDto>> GetAllAsync();
        Task<TEntityDto?> GetByIdAsync(long id);
        Task<TEntityDto> CreateAsync(TEntityDto dto);
        Task<TEntityDto?> UpdateAsync(TEntityDto dto);
        Task<bool> DeleteAsync(long id);
    }
}
