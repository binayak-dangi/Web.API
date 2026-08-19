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
    public class AdmHeadingController : ControllerBase
    {
        private readonly ILogger<AdmHeadingController> _logger;
        private readonly IAdmHeadingRepository _admHeadingRepository;

        public AdmHeadingController(ILogger<AdmHeadingController> logger, IAdmHeadingRepository admHeadingRepository)
        {
            _logger = logger;
            _admHeadingRepository = admHeadingRepository;
        }

        [HttpGet(Name = "GetAdmHeadings")]
        public async Task<IActionResult> GetAdmHeadings([FromQuery] PaginationModel pagination)
        {
            var headings = await _admHeadingRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<AdmHeadingDto>>
            {
                Success = true,
                Message = "Headings retrieved successfully.",
                Data = headings
            });
        }

        [HttpGet("{id}", Name = "GetAdmHeadingById")]
        public async Task<IActionResult> GetAdmHeadingById(long id)
        {
            var heading = await _admHeadingRepository.GetByIdAsync(id);
            if (heading == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Heading Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<AdmHeadingDto>
            {
                Success = true,
                Message = $"Heading is retrieved by Id: {id}",
                Data = heading
            });
        }

        [HttpPost(Name = "CreateAdmHeading")]
        public async Task<IActionResult> CreateAdmHeading([FromBody] AdmHeadingDto HeadingDto)
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


                if (await _admHeadingRepository.IsHeadingExist(null,HeadingDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Heading name already exists."
                    });
                }

                var Heading = await _admHeadingRepository.CreateAsync(HeadingDto);
                return Ok(new ApiResponseModel<AdmHeadingDto>
                {
                    Success = true,
                    Message = "Heading created successfully.",
                    Data = Heading
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

        [HttpPut("{id}", Name = "EditAdmHeading")]
        public async Task<IActionResult> EditAdmHeading(long id, [FromBody] AdmHeadingDto HeadingDto)
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

                if (await _admHeadingRepository.IsHeadingExist(id,HeadingDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Heading name already exists."
                    });
                }

                var heading = await _admHeadingRepository.UpdateAsync(id,HeadingDto);
                if (heading == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Heading with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<AdmHeadingDto>
                {
                    Success = true,
                    Message = $"Heading with ID {id} updated successfully.",
                    Data = heading
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating heading.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteAdmHeading")]
        public async Task<IActionResult> DeleteAdmHeading(long id)
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

                var deletedHeading = await _admHeadingRepository.SoftDeleteAsyncs(id);

                if (deletedHeading == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Heading with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<AdmHeadingDto>
                {
                    Success = true,
                    Message = $"Heading with Id {id} deleted successfully.",
                    Data = deletedHeading   
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Main Heading with Id {MainHeadingId}.", id);

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