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
    //[Authorize]
    public class HREmployeeController : ControllerBase
    {
        private readonly ILogger<HREmployeeController> _logger;
        private readonly IHREmployeeService _hrEmployeeService;

        public HREmployeeController(ILogger<HREmployeeController> logger,IHREmployeeService hrEmployeeService)
        {
            _logger = logger;
            _hrEmployeeService = hrEmployeeService;
        } 

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employee = await _hrEmployeeService.GetAllEmployeesAsync();

            return Ok(new ApiResponseModel<List<HREmployeeDto>>
            {
                Success = true,
                Message = "Employees retrieved successfully.",
                Data = employee
            });
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(long id)
        {
            var employee = await _hrEmployeeService.GetEmployeeByIdAsync(id);
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



                var employee = await _hrEmployeeService.CreateEmployeeAsync(employeeDto);

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

                employeeDto.Id = id;   // Set the ID from the route

                var employee = await _hrEmployeeService.UpdateEmployeeAsync(employeeDto);

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
                        Message = ex.InnerException?.Message ?? ex.Message,
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

                var deletedEmployee = await _hrEmployeeService.DeleteEmployeeAsync(id);

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
    }
}