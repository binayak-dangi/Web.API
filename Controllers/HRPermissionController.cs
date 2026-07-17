using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.DTOs;
using Web.API.Models;
using Web.API.Models.Common;
using Web.API.Models.DTOS;
using Web.API.Services.Interface;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HRPermissionController : ControllerBase
    {
        private readonly ILogger<HRPermissionController> _logger;
        private readonly IHRPermissionService _hrPermissionService;

        public HRPermissionController(ILogger<HRPermissionController> logger,IHRPermissionService hrPermissionService)
        {
            _logger = logger;
            _hrPermissionService = hrPermissionService;
        }

        [HttpGet(Name = "GetPermissions")]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _hrPermissionService.GetAllPermissionsAsync();

            return Ok(new ApiResponseModel<List<HRPermissionDto>>
            {
                Success = true,
                Message = "Permissions retrieved successfully.",
                Data = permissions
            });
        }

        //[HttpGet("{id}", Name = "GetPermissionById")]
        //public async Task<IActionResult> GetPermissionById(long id)
        //{
        //    var permission = await _hrPermissionService.GetPermissionByIdAsync(id);
        //    if (permission == null)
        //    {
        //        return BadRequest(new ApiResponseModel<object>
        //        {
        //            Success = false,
        //            Message = $"Invalid Permission Id: {id}",
        //            Data = { }
        //        });
        //    }

        //    return Ok(new ApiResponseModel<HRPermissionDto>
        //    {
        //        Success = true,
        //        Message = $"Permission is retrieved by Id: {id}",
        //        Data = permission
        //    });
        //}

        //[HttpPost(Name = "CreatePermission")]
        //public async Task<IActionResult> CreatePermission([FromBody] HRPermissionDto permissionDto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = "Invalid request.",
        //                Data = null
        //            });
        //        }



        //        var permission = await _hrPermissionService.CreatePermissionAsync(permissionDto);

        //        return Ok(new ApiResponseModel<HRPermissionDto>
        //        {
        //            Success = true,
        //            Message = "Permission created successfully.",
        //            Data = permission
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while creating permission.");

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = ex.InnerException?.Message ?? ex.Message,
        //                Data = null
        //            });
        //    }
        //}

        //[HttpPut("{id}", Name = "EditPermission")]
        //public async Task<IActionResult> EditPermission(long id, [FromBody] HRPermissionDto permissionDto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = "Invalid request.",
        //                Data = null
        //            });
        //        }

        //        permissionDto.Id = id;   // Set the ID from the route

        //        var permission = await _hrPermissionService.UpdatePermissionAsync(permissionDto);

        //        if (permission == null)
        //        {
        //            return NotFound(new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = $"Permission with ID {id} not found.",
        //                Data = null
        //            });
        //        }

        //        return Ok(new ApiResponseModel<HRPermissionDto>
        //        {
        //            Success = true,
        //            Message = $"Permission with ID {id} updated successfully.",
        //            Data = permission
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error updating permission.");

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = ex.InnerException?.Message ?? ex.Message,
        //                Data = null
        //            });
        //    }
        //}

        //[HttpDelete("{id}", Name = "DeletePermission")]
        //public async Task<IActionResult> DeletePermission(long id)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            return BadRequest(new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = "Invalid Permission Id.",
        //                Data = null
        //            });
        //        }

        //        var deletedPermission = await _hrPermissionService.DeletePermissionAsync(id);

        //        if (deletedPermission == null)
        //        {
        //            return NotFound(new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = $"Permission with Id {id} not found.",
        //                Data = null
        //            });
        //        }

        //        return Ok(new ApiResponseModel<HRPermissionDto>
        //        {
        //            Success = true,
        //            Message = $"Permission with Id {id} deleted successfully.",
        //            Data = deletedPermission
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while deleting permission with Id {PermissionId}.", id);

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponseModel<object>
        //            {
        //                Success = false,
        //                Message = ex.InnerException?.Message ?? ex.Message,
        //                Data = null
        //            });
        //    }
        //}
    }
}