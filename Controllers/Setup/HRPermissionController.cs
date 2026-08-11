using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Common;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Controllers.Setup
{
    [ApiController]
    [Route("api/[controller]")]

    [AllowAnonymous]
    //[Authorize]
    public class HRPermissionController : ControllerBase
    {
        private readonly ILogger<HRPermissionController> _logger;
        private readonly IHRPermissionRepository _hrPermissionService;

        public HRPermissionController(ILogger<HRPermissionController> logger, IHRPermissionRepository hrPermissionService)
        {
            _logger = logger;
            _hrPermissionService = hrPermissionService;
        }

        [HttpGet(Name = "GetPermissions")]
        public async Task<IActionResult> GetPermissions([FromQuery] PaginationModel pagination)
        {
            var permissions = await _hrPermissionService.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HRPermissionDto>>
            {
                Success = true,
                Message = "Permissions retrieved successfully.",
                Data = permissions
            });
        }

        [HttpGet("{id}", Name = "GetPermissionById")]
        public async Task<IActionResult> GetPermissionById(long id)
        {
            var permission = await _hrPermissionService.GetByIdAsync(id);
            if (permission == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Permission Id: {id}",
                    Data = null
                });
            }

            return Ok(new ApiResponseModel<HRPermissionDto>
            {
                Success = true,
                Message = $"Permission is retrieved by Id: {id}",
                Data = permission
            });
        }

        [HttpPut("{id}", Name = "EditPermission")]
        public async Task<IActionResult> EditPermission(long id, [FromBody] HRPermissionDto permissionDto)
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

                var permission = await _hrPermissionService.UpdateAsync(id, permissionDto);

                if (permission == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Permission with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRPermissionDto>
                {
                    Success = true,
                    Message = $"Permission with ID {id} updated successfully.",
                    Data = permission
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating permission.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        #region PermissionByRole

        [HttpGet("role/{id}/assigned", Name = "GetAssignedPermissionsByRole")]
        public async Task<IActionResult> GetAssignedPermissionsByRole(long? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Role Id is required."
                });
            }

            var permissions = await _hrPermissionService.GetPermissionsLst("HRPermissionByRole", "GetAssignedPermissionList", id.Value);

            return Ok(new ApiResponseModel<List<HRPermissionEmployeeRoleDto>>
            {
                Success = true,
                Message = "Assigned permissions retrieved successfully.",
                Data = permissions
            });
        }

        [HttpGet("role/{id}/all", Name = "GetAllPermissionsByRole")]
        public async Task<IActionResult> GetAllPermissionsByRole(long? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Role Id is required."
                });
            }

            var permissions = await _hrPermissionService.GetPermissionsLst("HRPermissionByRole", "GetAllPermissionList", id.Value);

            return Ok(new ApiResponseModel<List<HRPermissionEmployeeRoleDto>>
            {
                Success = true,
                Message = "All permissions retrieved successfully.",
                Data = permissions
            });
        }

        [HttpPut("bulkedit/{id}", Name = "BulkEditPermissionByRole")]
        public async Task<IActionResult> BulkEditPermissionByRole(long id, [FromBody] List<HRRolePermissionLinkMirror> permissionDtos)
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

                if (permissionDtos == null || !permissionDtos.Any())
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Permission list cannot be empty.",
                        Data = null
                    });
                }

                var rolePermissionLinks = permissionDtos.Select(item => new HRRolePermissionLinkMirror
                {
                    IdHRPermission = item.IdHRPermission,
                    CreateOnly = item.CreateOnly,
                    EditOnly = item.EditOnly,
                    DeleteOnly = item.DeleteOnly,
                    ReadOnly = item.ReadOnly,
                    IdHRCompany = item.IdHRCompany,
                    IdHRRole = id
                }).ToList();

                await _hrPermissionService.CreateRolePermisionLinkAsync(rolePermissionLinks);

                var result = await _hrPermissionService.GetPermissionsLst("HRPermissionByRole", "BulkUpdatePermissionList", id);

                return Ok(new ApiResponseModel<object>
                {
                    Success = true,
                    Message = "Permissions updated successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating permissions for role {RoleId}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "An error occurred while updating permissions.",
                        Data = null
                    });
            }
        }
        #endregion

        #region PermissionByEmployee

        [HttpGet("employee/{id}/assigned", Name = "GetAssignedPermissionsByEmployee")]
        public async Task<IActionResult> GetAssignedPermissionsByEmployee(long? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Role Id is required."
                });
            }

            var permissions = await _hrPermissionService.GetPermissionsLst("HRPermissionByEmployee", "GetAssignedPermissionList", id.Value);

            return Ok(new ApiResponseModel<List<HRPermissionEmployeeRoleDto>>
            {
                Success = true,
                Message = "Assigned permissions retrieved successfully.",
                Data = permissions
            });
        }

        [HttpGet("employee/{id}/all", Name = "GetAllPermissionsByEmployee")]
        public async Task<IActionResult> GetAllPermissionsByEmployee(long? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Role Id is required."
                });
            }

            var permissions = await _hrPermissionService.GetPermissionsLst("HRPermissionByEmployee", "GetAllPermissionList", id.Value);

            return Ok(new ApiResponseModel<List<HRPermissionEmployeeRoleDto>>
            {
                Success = true,
                Message = "All permissions retrieved successfully.",
                Data = permissions
            });
        }

        [HttpPut("bulkedit/employee/{id}", Name = "BulkEditPermissionByEmployee")]
        public async Task<IActionResult> BulkEditPermissionByEmployee(long id, [FromBody] List<HRRolePermissionLinkMirror> permissionDtos)
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

                if (permissionDtos == null || !permissionDtos.Any())
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Permission list cannot be empty.",
                        Data = null
                    });
                }

                var employeePermissionLinks = permissionDtos.Select(item => new HREmployeePermissionLinkMirror
                {
                    IdHRPermission = item.IdHRPermission,
                    CreateOnly = item.CreateOnly,
                    EditOnly = item.EditOnly,
                    DeleteOnly = item.DeleteOnly,
                    ReadOnly = item.ReadOnly,
                    IdHRCompany = item.IdHRCompany,
                    IdHREmployee = id
                }).ToList();

                await _hrPermissionService.CreateEmployeePermissionLinkAsync(employeePermissionLinks);

                var result = await _hrPermissionService.GetPermissionsLst("HRPermissionByEmployee", "BulkUpdatePermissionList", id);

                return Ok(new ApiResponseModel<object>
                {
                    Success = true,
                    Message = "Permissions updated successfully.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating permissions for role {RoleId}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "An error occurred while updating permissions.",
                        Data = null
                    });
            }
        }
        #endregion

        #region Menu
        [HttpGet("menu/{id}", Name = "GetMenuPermissions")]
        public async Task<IActionResult> GetMenuPermissions(long? id)
        {
            if (!id.HasValue)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Employee Id is required."
                });
            }

            var permissions = await _hrPermissionService.GetPermissionsLst("GetPermissionMenuLst", "", id.Value);

            return Ok(new ApiResponseModel<List<HRPermissionEmployeeRoleDto>>
            {
                Success = true,
                Message = "All menus retrieved successfully.",
                Data = permissions
            });
        }

        #endregion

    }
}