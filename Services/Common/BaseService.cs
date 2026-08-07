using Web.API.Models;
using Web.API.Repositories.Common;

namespace Web.API.Services.Common
{
  

    public class BaseService<TEntity, TDto> : IBaseService<TDto> where TEntity: CommonModel
    {
        protected readonly IBaseRepository< TDto> _baseRepository;

        public BaseService(IBaseRepository<TDto> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        // Get All (Exclude Deleted Records)
        public virtual async Task<List<TDto>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        // Get By Id (Exclude Deleted Records)
        public virtual async Task<TDto?> GetByIdAsync(long id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        // Create
        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            return await _baseRepository.CreateAsync(dto);
        }

        // Update
        public virtual async Task<TDto?> UpdateAsync(long id, TDto dto)
        {
            return await _baseRepository.UpdateAsync(id, dto);
        }

        // Soft Delete
        public virtual async Task<TDto?> SoftDeleteAsyncs(long id)
        {
            return await _baseRepository.SoftDeleteAsyncs(id);
        }
    }
}