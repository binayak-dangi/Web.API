using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.API.Models;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Implementations;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Controllers.Setup
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    //[AllowAnonymous]
    public class HREmployeeController : ControllerBase
    {
        private readonly ILogger<HREmployeeController> _logger;
        private readonly IHREmployeeRepository _employeeRepository;


        public HREmployeeController(ILogger<HREmployeeController> logger, IHREmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] PaginationModel pagination)
        {
            var employee = await _employeeRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HREmployeeDto>>
            {
                Success = true,
                Message = "Employees retrieved successfully.",
                Data = employee
            });
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(long id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Employee Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HREmployeeDto>
            {
                Success = true,
                Message = $"Employee is retrieved by Id: {id}",
                Data = employee
            });
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] HREmployeeDto employeeDto)
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

                if(await _employeeRepository.IsUsernameAvailableAsync(employeeDto.Username) == false)
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Username is already taken.",
                        Data = null
                    });
                }

                var employee = await _employeeRepository.CreateEmployeeAsync(employeeDto);

                return Ok(new ApiResponseModel<HREmployeeDto>
                {
                    Success = true,
                    Message = "Employee created successfully.",
                    Data = employee
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        [HttpPut("{id}", Name = "EditEmployee")]
        public async Task<IActionResult> EditEmployee(long id, [FromBody] HREmployeeDto employeeDto)
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

                var employee = await _employeeRepository.UpdateAsync(id, employeeDto);

                if (employee == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Employee with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HREmployeeDto>
                {
                    Success = true,
                    Message = $"Employee with ID {id} updated successfully.",
                    Data = employee
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid Employee Id.",
                        Data = null
                    });
                }

                var deletedEmployee = await _employeeRepository.SoftDeleteAsyncs(id);

                if (deletedEmployee == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Employee with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HREmployeeDto>
                {
                    Success = true,
                    Message = $"Employee with Id {id} deleted successfully.",
                    Data = deletedEmployee
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting employee with Id {EmployeeId}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        [HttpPost("reset-password/{id:long}")]
        public async Task<IActionResult> ResetPassword(long id)
        {
            try
            {
                var roleName = User.FindFirstValue(ClaimTypes.Role);

                if (!string.Equals(roleName, "System", StringComparison.OrdinalIgnoreCase) && !string.Equals(roleName, "Admin", StringComparison.OrdinalIgnoreCase))
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        new ApiResponseModel<object>
                        {
                            Success = false,
                            Message = "You do not have permission to reset passwords.",
                            Data = null
                        });
                }

                var employeeDto = new HREmployeeDto
                {
                    Id = id
                };

                var result = await _employeeRepository.ResetPasswordAsync(id);

                if (result == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Employee not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<object>
                {
                    Success = true,
                    Message = "Password reset successfully. Employee must set a new password on next login.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while resetting password for employee {EmployeeId}.",
                    id);

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "An error occurred while resetting the password.",
                        Data = null
                    });
            }
        }

        //[HttpGet("check-username")]
        //public async Task<IActionResult> CheckUsername(string username)
        //{
        //    if (string.IsNullOrWhiteSpace(username))
        //    {
        //        return BadRequest(new ApiResponseModel<object>
        //        {
        //            Success = false,
        //            Message = "Username is required.",
        //            Data = null
        //        });
        //    }

        //    username = username.Trim().ToLower();

        //    if (username.Length < 5)
        //    {
        //        return Ok(new ApiResponseModel<object>
        //        {
        //            Success = true,
        //            Message = "Username must be at least 5 characters long.",
        //            Data = new
        //            {
        //                IsAvailable = false
        //            }
        //        });
        //    }

        //    bool isAvailable =
        //        await _employeeRepository.IsUsernameAvailableAsync(username);

        //    return Ok(new ApiResponseModel<object>
        //    {
        //        Success = true,
        //        Message = isAvailable
        //            ? "Username is available."
        //            : "Username is unavailable.",
        //        Data = new
        //        {
        //            IsAvailable = isAvailable
        //        }
        //    });
        //}
       
    }
}