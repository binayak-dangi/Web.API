using AutoMapper;
using MyTestMvc.Models.Setup;
using Web.API.Data;
using Web.API.DTOs;
using Web.API.Models.DTOS;
using Web.API.Services.CommonService.Implementation;
using Web.API.Services.Interface;

namespace Web.API.Services.Implementation
{
    public class HRBranchService : GenericService<HRBranch, HRBranchDto>, IHRBranchService
    {
        public HRBranchService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<List<HRBranchDto>> GetAllBranchesAsync()
            => GetAllAsync();

        public Task<HRBranchDto?> GetBranchByIdAsync(long id)
            => GetByIdAsync(id);

        public Task<HRBranchDto> CreateBranchAsync(HRBranchDto dto)
            => CreateAsync(dto);

        public Task<HRBranchDto?> UpdateBranchAsync(HRBranchDto dto)
            => UpdateAsync(dto);

        public Task<HRBranchDto> DeleteBranchAsync(long id)
            => SoftDeleteAsync(id);
    }
}