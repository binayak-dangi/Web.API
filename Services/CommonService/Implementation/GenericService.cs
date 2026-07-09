using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.CommonService.Implementation
{
    public class GenericService<TEntity, TDto> : IGenericService<TDto>  where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return default;

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto?> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}