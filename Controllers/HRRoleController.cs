using Microsoft.AspNetCore.Mvc;
using Web.API.DTOs;
using Web.API.Models;
using Web.API.Models.Common;
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

        [HttpGet(Name = "GetRoles")]
        public async Task<IActionResult> GetRole()
        {
            var roles = await _hrRoleService.GetAllRolesAsync();

            return Ok(new ApiResponseModel<List<HRRoleDto>>
            {
                Success = true,
                Message = "Roles retrieved successfully.",
                Data = roles
            });
        }

        [HttpPost(Name = "CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] HRRoleDto roleDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid request.",
                        Data = null
                    });
                }

                

                var role = await _hrRoleService.CreateRoleAsync(roleDto);

                return Ok(new ApiResponseModel<HRRoleDto>
                {
                    Success = true,
                    Message = "Role created successfully.",
                    Data = role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating role.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
        }
    }
}