using AutoMapper;
using Web.API.Data;

using Web.API.Services.CommonService.Implementation;
using Web.API.Models.Entities.Setup;
using Web.API.Models.DTOS.Setup;
using Web.API.Services.Interface.Setup;

namespace Web.API.Services.Implementation.Setup
{
    public class HRBranchService : GenericService<HRBranch, HRBranchDto>, IHRBranchService
    {
        public HRBranchService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        
    }
}