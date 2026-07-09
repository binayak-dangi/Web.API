using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyTestMvc.Models;
using Web.API.Data;
using Web.API.Models.Common;
using Web.API.Services.CommonService.Interface;

namespace Web.API.Services.CommonService.Implementation
{
    public class GenericService<TEntity, TDto> : IGenericService<TDto> where TEntity : CommonModel
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

        // Get All (Exclude Deleted Records)
        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }


        // Get By Id (Exclude Deleted Records)
        public virtual async Task<TDto?> GetByIdAsync(long id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null)
                return default;

            return _mapper.Map<TDto>(entity);
        }


        // Create
        public virtual async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            entity.IsDeleted = false;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }


        // Update
        public virtual async Task<TDto?> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }


        // Hard Delete (Permanent Delete)
        public virtual async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }


        // Soft Delete
        public virtual async Task<bool> SoftDeleteAsync(long id)
        {
            var entity = await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}