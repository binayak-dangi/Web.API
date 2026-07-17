using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.DTOs;
using Web.API.Models;
using Web.API.Models.Common;
using Web.API.Services.Interface;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HRRoleController : ControllerBase
    {
        private readonly ILogger<HRRoleController> _logger;
        private readonly IHRRoleService _hrRoleService;

        public HRRoleController(ILogger<HRRoleController> logger,IHRRoleService hrRoleService)
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

        [HttpGet("{id}", Name = "GetRoleById")]
        public async Task<IActionResult> GetRoleById(long id)
        {
            var role = await _hrRoleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Role Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HRRoleDto>
            {
                Success = true,
                Message = $"Roles is retrieved by Id: {id}",
                Data = role
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
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null 
                    });
            }
        }

        [HttpPut("{id}", Name = "EditRole")]
        public async Task<IActionResult> EditRole(long id, [FromBody] HRRoleDto roleDto)
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

                roleDto.Id = id;   // Set the ID from the route

                var role = await _hrRoleService.UpdateRoleAsync(roleDto);

                if (role == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Role with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRRoleDto>
                {
                    Success = true,
                    Message = $"Role with ID {id} updated successfully.",
                    Data = role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteRole")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid Role Id.",
                        Data = null
                    });
                }

                var deletedRole = await _hrRoleService.DeleteRoleAsync(id);

                if (deletedRole == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Role with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRRoleDto>
                {
                    Success = true,
                    Message = $"Role with Id {id} deleted successfully.",
                    Data = deletedRole
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting role with Id {RoleId}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }
    }
}