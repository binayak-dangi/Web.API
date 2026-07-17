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
    public class HRCompanyController : ControllerBase
    {
        private readonly ILogger<HRCompanyController> _logger;
        private readonly IHRCompanyService _hrCompanyService;

        public HRCompanyController(ILogger<HRCompanyController> logger,IHRCompanyService hrCompanyService)
        {
            _logger = logger;
            _hrCompanyService = hrCompanyService;
        }

        [HttpGet(Name = "GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _hrCompanyService.GetAllCompaniesAsync();

            return Ok(new ApiResponseModel<List<HRCompanyDto>>
            {
                Success = true,
                Message = "Companies retrieved successfully.",
                Data = companies
            });
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(long id)
        {
            var company = await _hrCompanyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Company Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HRCompanyDto>
            {
                Success = true,
                Message = $"Company is retrieved by Id: {id}",
                Data = company
            });
        }

        [HttpPost(Name = "CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] HRCompanyDto companyDto)
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



                var company = await _hrCompanyService.CreateCompanyAsync(companyDto);

                return Ok(new ApiResponseModel<HRCompanyDto>
                {
                    Success = true,
                    Message = "Company created successfully.",
                    Data = company
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating branch.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        [HttpPut("{id}", Name = "EditCompany")]
        public async Task<IActionResult> EditCompany(long id, [FromBody] HRCompanyDto companyDto)
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

                companyDto.Id = id;   // Set the ID from the route

                var company = await _hrCompanyService.UpdateCompanyAsync(companyDto);

                if (company == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Company with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRCompanyDto>
                {
                    Success = true,
                    Message = $"Company with ID {id} updated successfully.",
                    Data = company
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

        [HttpDelete("{id}", Name = "DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid Branch Id.",
                        Data = null
                    });
                }

                var deletedCompany = await _hrCompanyService.DeleteCompanyAsync(id);

                if (deletedCompany == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Company with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRCompanyDto>
                {
                    Success = true,
                    Message = $"Company with Id {id} deleted successfully.",
                    Data = deletedCompany
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting company with Id {CompanyId}.", id);

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