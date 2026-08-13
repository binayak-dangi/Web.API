using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;
using Web.API.Services.Setup.Interfaces;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MyProfileController : ControllerBase
{
    private readonly IHREmployeeRepository _employeeRepository;
    private readonly IAuthRepository _authRepository;
    private readonly ILogger<MyProfileController> _logger;  

    public MyProfileController(IHREmployeeRepository employeeRepository, IAuthRepository authRepository, ILogger<MyProfileController> logger)
    {
        _employeeRepository = employeeRepository;
        _authRepository = authRepository;
        _logger = logger;
    }

    //PUT: api/MyProfile/account
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {

        long IdUserSession = 0;
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

            var IdUserSessionClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!long.TryParse(IdUserSessionClaim, out IdUserSession))
            {
                return Unauthorized(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Invalid user session.",
                    Data = null
                });
            }

            var result = await _authRepository.ChangePasswordAsync(IdUserSession, dto);

            if (!result)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Current password is incorrect.",
                    Data = null
                });
            }

            return Ok(new ApiResponseModel<object>
            {
                Success = true,
                Message = "Password changed successfully. Please login again.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred while changing password for employee {EmployeeId}.", IdUserSession);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ApiResponseModel<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });
        }
    }


    // GET: api/MyProfile/account
    [HttpGet("account")]
    public async Task<IActionResult> GetMyAccount()
    {
        try
        {
            var employeeIdClaim = User.FindFirst("IDHREmployee")?.Value;

            if (!long.TryParse(employeeIdClaim, out long employeeId))
            {
                return Unauthorized(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Invalid employee information.",
                    Data = null
                });
            }

            var employee = await _employeeRepository.GetMyProfileAsync(employeeId);

            if (employee == null)
            {
                return NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Account not found.",
                    Data = null
                });
            }

            return Ok(new ApiResponseModel<HREmployeeDto>
            {
                Success = true,
                Message = "Account retrieved successfully.",
                Data = employee
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponseModel<object>
            {
                Success = false,
                Message = "An error occurred while retrieving account.",
                Data = null
            });
        }
    }


    // PUT: api/MyProfile
    [HttpPut("account")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMyProfileDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Invalid request.",
                    Data = ModelState
                });
            }

            var employeeIdClaim = User.FindFirst("IDHREmployee")?.Value;

            if (!long.TryParse(employeeIdClaim, out long employeeId))
            {
                return Unauthorized(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Invalid employee information.",
                    Data = null
                });
            }

            var result = await _employeeRepository.UpdateMyProfileAsync(
                employeeId,
                dto);

            if (result == null)
            {
                return NotFound(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Profile not found.",
                    Data = null
                });
            }

            return Ok(new ApiResponseModel<HREmployeeDto>
            {
                Success = true,
                Message = "Profile updated successfully.",
                Data = result
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponseModel<object>
            {
                Success = false,
                Message = "An error occurred while updating profile.",
                Data = null
            });
        }
    }
}