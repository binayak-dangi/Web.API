using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Setup.Interfaces;
using Web.API.Services.Common;
using Web.API.Services.Setup.Interfaces;

namespace Web.API.Services.Setup.Implementations
{
    public class HRBranchService : BaseService<HRBranch, HRBranchDto>, IHRBranchService
    {
        private readonly IHRBranchRepository _hrBranchRepository;

        public HRBranchService(IHRBranchRepository hrBranchRepository) : base(hrBranchRepository)
        {
            _hrBranchRepository = hrBranchRepository;
        }

        //public async Task<bool> IsBranchExist(HRBranchDto dto)
        //{
        //    return await _hrBranchRepository.IsBranchExist(dto);
        //}
    }
}