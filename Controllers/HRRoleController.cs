using Microsoft.AspNetCore.Mvc;
using Web.API.Services.Interface;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HRRoleController : ControllerBase
    {
        private readonly ILogger<HRRoleController> _logger;
        private readonly IHRRoleService _hrRoleService;

        public HRRoleController(
            ILogger<HRRoleController> logger,
            IHRRoleService hrRoleService)
        {
            _logger = logger;
            _hrRoleService = hrRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _hrRoleService.GetAllRolesAsync();

            return Ok(roles);
        }
    }
}