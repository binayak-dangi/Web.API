using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Common;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Repositories.Setup.Implementations
{
   

    public class HRBranchRepository : BaseRepository<HRBranch, HRBranchDto>, IHRBranchRepository
    {
        public HRBranchRepository(AppDbContext context, IMapper mapper, IValidator<HRBranchDto> validator)
            : base(context, mapper, validator)
        {
        }

        public async Task<bool> IsBranchExist(long? id, HRBranchDto dto)
        {
            var result= await _context.HRBranch
                .AnyAsync(x => x.BranchName == dto.BranchName && (!id.HasValue || x.Id != id.Value));
            return result;
        }

    }
}