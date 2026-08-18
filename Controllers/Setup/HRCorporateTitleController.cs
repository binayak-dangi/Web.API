using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models;
using Web.API.Models.Common;
using Web.API.Models.DTOS;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Controllers.Setup
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HRCorporateTitleController : ControllerBase
    {
        private readonly ILogger<HRCorporateTitleController> _logger;
        private readonly IHRCorporateTitleRepository _hrCorporateTitleRepository;

        public HRCorporateTitleController(ILogger<HRCorporateTitleController> logger,IHRCorporateTitleRepository hrCorporateTitleRepository)
        {
            _logger = logger;
            _hrCorporateTitleRepository = hrCorporateTitleRepository;
        }

        [HttpGet(Name = "GetCorporateTitles")]
        public async Task<IActionResult> GetCorporateTitles([FromQuery] PaginationModel pagination)
        {
            var corporateTitles = await _hrCorporateTitleRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HRCorporateTitleDto>>
            {
                Success = true,
                Message = "Corporate titles retrieved successfully.",
                Data = corporateTitles
            });
        }

        [HttpGet("{id}", Name = "GetCorporateTitleById")]
        public async Task<IActionResult> GetCorporateTitleById(long id)
        {
            var corporateTitle = await _hrCorporateTitleRepository.GetByIdAsync(id);
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

                if (await _hrCorporateTitleRepository.IsCorporateTitleExist(null, corporateTitleDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Corporate title already exists."
                    });
                }

                var corporateTitle = await _hrCorporateTitleRepository.CreateAsync(corporateTitleDto);

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

                if (await _hrCorporateTitleRepository.IsCorporateTitleExist(id, corporateTitleDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Corporate title already exists."
                    });
                }

                var corporateTitle = await _hrCorporateTitleRepository.UpdateAsync(id,corporateTitleDto);

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

                var deletedCorporateTitle = await _hrCorporateTitleRepository.SoftDeleteAsyncs(id);

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