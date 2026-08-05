using AutoMapper;
using Web.API.Data;
using FluentValidation;
using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Services.Implementation.Setup
{
    public class HRBranchService : GenericService<HRBranch, HRBranchDto>, IHRBranchService
    {
        public HRBranchService(AppDbContext context, IMapper mapper, IValidator<HRBranchDto> validator)
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