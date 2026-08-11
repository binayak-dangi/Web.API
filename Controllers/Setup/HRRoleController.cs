using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Controllers.Setup
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class HRRoleController : ControllerBase
    {
        private readonly ILogger<HRRoleController> _logger;
        private readonly IHRRoleRepository _hrRoleService;

        public HRRoleController(ILogger<HRRoleController> logger,IHRRoleRepository hrRoleService)
        {
            _logger = logger;
            _hrRoleService = hrRoleService;
        }

        [HttpGet(Name = "GetRoles")]
        public async Task<IActionResult> GetRole([FromQuery] PaginationModel pagination)
        {
            var roles = await _hrRoleService.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HRRoleDto>>
            {
                Success = true,
                Message = "Roles retrieved successfully.",
                Data = roles
            });
        }

        [HttpGet("{id}", Name = "GetRoleById")]
        public async Task<IActionResult> GetRoleById(long id)
        {
            var role = await _hrRoleService.GetByIdAsync(id);
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



                var role = await _hrRoleService.CreateAsync(roleDto);

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

                var role = await _hrRoleService.UpdateAsync(id, roleDto);

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

                var deletedRole = await _hrRoleService.SoftDeleteAsyncs(id);

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