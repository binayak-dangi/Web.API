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

        public async Task<bool> IsBranchExist(HRBranchDto dto)
        {
            bool exists;

            if (dto.Id > 0) // Update
            {
                exists = await _context.HRBranch.AnyAsync(x => x.BranchName == dto.BranchName && x.Id != dto.Id);
            }
            else // Create
            {
                exists = await _context.HRBranch.AnyAsync(x => x.BranchName == dto.BranchName);
            }

            return exists;
        }

    }
}