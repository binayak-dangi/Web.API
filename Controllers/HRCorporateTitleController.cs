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
    public class HRCorporateTitleController : ControllerBase
    {
        private readonly ILogger<HRCorporateTitleController> _logger;
        private readonly IHRCorporateTitleService _hrCorporateTitleService;

        public HRCorporateTitleController(ILogger<HRCorporateTitleController> logger,IHRCorporateTitleService hrCorporateTitleService)
        {
            _logger = logger;
            _hrCorporateTitleService = hrCorporateTitleService;
        }

        [HttpGet(Name = "GetCorporateTitles")]
        public async Task<IActionResult> GetCorporateTitles()
        {
            var corporateTitles = await _hrCorporateTitleService.GetAllCorporateTitlesAsync();

            return Ok(new ApiResponseModel<List<HRCorporateTitleDto>>
            {
                Success = true,
                Message = "Corporate titles retrieved successfully.",
                Data = corporateTitles
            });
        }

        [HttpGet("{id}", Name = "GetCorporateTitleById")]
        public async Task<IActionResult> GetCorporateTitleById(long id)
        {
            var corporateTitle = await _hrCorporateTitleService.GetCorporateTitleByIdAsync(id);
            if (corporateTitle == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Corporate Title Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HRCorporateTitleDto>
            {
                Success = true,
                Message = $"Corporate Title is retrieved by Id: {id}",
                Data = corporateTitle
            });
        }

        [HttpPost(Name = "CreateCorporateTitle")]
        public async Task<IActionResult> CreateCorporateTitle([FromBody] HRCorporateTitleDto corporateTitleDto)
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

                var corporateTitle = await _hrCorporateTitleService.CreateCorporateTitleAsync(corporateTitleDto);

                return Ok(new ApiResponseModel<HRCorporateTitleDto>
                {
                    Success = true,
                    Message = "Corporate title created successfully.",
                    Data = corporateTitle
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

        [HttpPut("{id}", Name = "EditCorporateTitle")]
        public async Task<IActionResult> EditCorporateTitle(long id, [FromBody] HRCorporateTitleDto corporateTitleDto)
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

                corporateTitleDto.Id = id;   // Set the ID from the route

                var corporateTitle = await _hrCorporateTitleService.UpdateCorporateTitleAsync(corporateTitleDto);

                if (corporateTitle == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Corporate Title with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRCorporateTitleDto>
                {
                    Success = true,
                    Message = $"Corporate Title with ID {id} updated successfully.",
                    Data = corporateTitle
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating corporate title.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteCorporateTitle")]
        public async Task<IActionResult> DeleteCorporateTitle(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid Corporate Title Id.",
                        Data = null
                    });
                }

                var deletedCorporateTitle = await _hrCorporateTitleService.DeleteCorporateTitleAsync(id);

                if (deletedCorporateTitle == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Corporate Title with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRCorporateTitleDto>
                {
                    Success = true,
                    Message = $"Corporate Title with Id {id} deleted successfully.",
                    Data = deletedCorporateTitle
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting corporate title with Id {CorporateTitleId}.", id);

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