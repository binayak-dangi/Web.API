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
    public class HRFunctionalTitleController : ControllerBase
    {
        private readonly ILogger<HRFunctionalTitleController> _logger;
        private readonly IHRFunctionalTitleService _hrFunctionalTitleService;

        public HRFunctionalTitleController(ILogger<HRFunctionalTitleController> logger,IHRFunctionalTitleService hrFunctionalTitleService)
        {
            _logger = logger;
            _hrFunctionalTitleService = hrFunctionalTitleService;
        }

        [HttpGet(Name = "GetFunctionalTitles")]
        public async Task<IActionResult> GetFunctionalTitles()
        {
            var functionalTitles = await _hrFunctionalTitleService.GetAllFunctionalTitlesAsync();

            return Ok(new ApiResponseModel<List<HRFunctionalTitleDto>>
            {
                Success = true,
                Message = "Functional titles retrieved successfully.",
                Data = functionalTitles
            });
        }

        [HttpGet("{id}", Name = "GetFunctionalTitleById")]
        public async Task<IActionResult> GetFunctionalTitleById(long id)
        {
            var functionalTitle = await _hrFunctionalTitleService.GetFunctionalTitleByIdAsync(id);
            if (functionalTitle == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Functional Title Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HRFunctionalTitleDto>
            {
                Success = true,
                Message = $"Functional Title is retrieved by Id: {id}",
                Data = functionalTitle
            });
        }

        [HttpPost(Name = "CreateFunctionalTitle")]
        public async Task<IActionResult> CreateFunctionalTitle([FromBody] HRFunctionalTitleDto functionalTitleDto)
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

                var functionalTitle = await _hrFunctionalTitleService.CreateFunctionalTitleAsync(functionalTitleDto);

                return Ok(new ApiResponseModel<HRFunctionalTitleDto>
                {
                    Success = true,
                    Message = "Functional title created successfully.",
                    Data = functionalTitle
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

        [HttpPut("{id}", Name = "EditFunctionalTitle")]
        public async Task<IActionResult> EditFunctionalTitle(long id, [FromBody] HRFunctionalTitleDto functionalTitleDto)
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

                functionalTitleDto.Id = id;   // Set the ID from the route

                var functionalTitle = await _hrFunctionalTitleService.UpdateFunctionalTitleAsync(functionalTitleDto);

                if (functionalTitle == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Functional Title with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRFunctionalTitleDto>
                {
                    Success = true,
                    Message = $"Functional Title with ID {id} updated successfully.",
                    Data = functionalTitle
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating functional title.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteFunctionalTitle")]
        public async Task<IActionResult> DeleteFunctionalTitle(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid Functional Title Id.",
                        Data = null
                    });
                }

                var deletedFunctionalTitle = await _hrFunctionalTitleService.DeleteFunctionalTitleAsync(id);

                if (deletedFunctionalTitle == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Functional Title with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRFunctionalTitleDto>
                {
                    Success = true,
                    Message = $"Functional Title with Id {id} deleted successfully.",
                    Data = deletedFunctionalTitle
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting functional title with Id {FunctionalTitleId}.", id);

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