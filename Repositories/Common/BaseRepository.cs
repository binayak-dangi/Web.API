using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web.API.Data;
using Web.API.Models;
using Web.API.Models.Common;

namespace Web.API.Repositories.Common
{
    public class BaseRepository<TEntity, TDto> : IBaseRepository<TDto> where TEntity : CommonModel
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly IValidator<TDto> _validator;

        public BaseRepository(AppDbContext context, IMapper mapper, IValidator<TDto> validator)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = _context.Set<TEntity>();
            _validator = validator;
        }

        //Validation method to be called before Create or Update operations
        protected virtual async Task ValidateAsync(TDto dto)
        {
            if (_validator == null)
                return;

            var result = await _validator.ValidateAsync(dto);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        //search
        protected virtual IQueryable<TEntity> ApplySearch(IQueryable<TEntity> query,string? searchKey)
        {
            return query;
        }

        // Get All (Exclude Deleted Records)
        public virtual async Task<PagedResult<TDto>> GetAllAsync(PaginationModel pagination)
        {
            var page = pagination.Page ?? 1;
            var pageSize = pagination.PageSize ?? 10;


            // Base query
            var query = _dbSet.AsNoTracking().Where(x => !x.IsDeleted);

            // Search
            query = ApplySearch(query, pagination.SearchKey);

            // Total records
            var totalRecords = await query.CountAsync();

            // Sorting
            query = query.ApplySorting(pagination.Sort, pagination.SortDir);

            // Pagination
            var entities = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            // Map Entity -> DTO
            var data = _mapper.Map<List<TDto>>(entities);

            // Result
            return new PagedResult<TDto>
            {
                Data = data,
                Page = page,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };
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
            await ValidateAsync(dto);

            var entity = _mapper.Map<TEntity>(dto);
            entity.IsDeleted = false;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }

        // Update
        public virtual async Task<TDto?> UpdateAsync(long id, TDto dto)
        {
            // Validate
            await ValidateAsync(dto);

            // Find existing entity
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return default;

            // Copy DTO values into existing entity
            _mapper.Map(dto, entity);

            // Save changes
            await _context.SaveChangesAsync();

            // Return updated DTO
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
        public virtual async Task<TDto?> SoftDeleteAsyncs(long id)
        {
            var entity = await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (entity == null)
                return default;

            entity.IsDeleted = true;

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(entity);
        }
    }
}