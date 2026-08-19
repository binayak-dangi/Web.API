using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Controllers.Setup
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AdmMainHeadingController : ControllerBase
    {
        private readonly ILogger<AdmMainHeadingController> _logger;
        private readonly IAdmMainHeadingRepository _admMainHeadingRepository;

        public AdmMainHeadingController(ILogger<AdmMainHeadingController> logger, IAdmMainHeadingRepository admMainHeadingRepository)
        {
            _logger = logger;
            _admMainHeadingRepository = admMainHeadingRepository;
        }

        [HttpGet(Name = "GetAdmMainHeadings")]
        public async Task<IActionResult> GetAdmMainHeadings([FromQuery] PaginationModel pagination)
        {
            var mainHeadings = await _admMainHeadingRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<AdmMainHeadingDto>>
            {
                Success = true,
                Message = "Main Headings retrieved successfully.",
                Data = mainHeadings
            });
        }

        [HttpGet("{id}", Name = "GetAdmMainHeadingById")]
        public async Task<IActionResult> GetAdmMainHeadingById(long id)
        {
            var mainHeading = await _admMainHeadingRepository.GetByIdAsync(id);
            if (mainHeading == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Main Heading Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<AdmMainHeadingDto>
            {
                Success = true,
                Message = $"Main Heading is retrieved by Id: {id}",
                Data = mainHeading
            });
        }

        [HttpPost(Name = "CreateAdmMainHeading")]
        public async Task<IActionResult> CreateAdmMainHeading([FromBody] AdmMainHeadingDto mainHeadingDto)
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


                if (await _admMainHeadingRepository.IsMainHeadingExist(null,mainHeadingDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Main Heading name already exists."
                    });
                }

                var mainHeading = await _admMainHeadingRepository.CreateAsync(mainHeadingDto);
                return Ok(new ApiResponseModel<AdmMainHeadingDto>
                {
                    Success = true,
                    Message = "Main Heading created successfully.",
                    Data = mainHeadingDto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating branch.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.Message,
                    });
            }
        }

        [HttpPut("{id}", Name = "EditAdmMainHeading")]
        public async Task<IActionResult> EditAdmMainHeading(long id, [FromBody] AdmMainHeadingDto mainHeadingDto)
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

                if (await _admMainHeadingRepository.IsMainHeadingExist(id,mainHeadingDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Main Heading name already exists."
                    });
                }

                var mainHeading = await _admMainHeadingRepository.UpdateAsync(id,mainHeadingDto);
                if (mainHeading == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Main Heading with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<AdmMainHeadingDto>
                {
                    Success = true,
                    Message = $"Main Heading with ID {id} updated successfully.",
                    Data = mainHeading
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

        [HttpDelete("{id}", Name = "DeleteAdmMainHeading")]
        public async Task<IActionResult> DeleteAdmMainHeading(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid Main Heading Id.",
                        Data = null
                    });
                }

                var deletedMainHeading = await _admMainHeadingRepository.SoftDeleteAsyncs(id);

                if (deletedMainHeading == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Main Heading with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<AdmMainHeadingDto>
                {
                    Success = true,
                    Message = $"Main Heading with Id {id} deleted successfully.",
                    Data = deletedMainHeading
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Main Heading with Id {MainHeadingId}.", id);

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