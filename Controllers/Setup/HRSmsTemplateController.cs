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
    public class HRSmsTemplateController : ControllerBase
    {
        private readonly ILogger<HRSmsTemplateController> _logger;
        private readonly IHRSmsTemplateRepository _hrSmsTemplateRepository;

        public HRSmsTemplateController(ILogger<HRSmsTemplateController> logger, IHRSmsTemplateRepository hrSmsTemplateRepository)
        {
            _logger = logger;
            _hrSmsTemplateRepository = hrSmsTemplateRepository;
        }

        [HttpGet(Name = "GetHRSmsTemplates")]
        public async Task<IActionResult> GetHRSmsTemplates([FromQuery] PaginationModel pagination)
        {
            var elements = await _hrSmsTemplateRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HRSmsTemplateDto>>
            {
                Success = true,
                Message = "Sms Template retrieved successfully.",
                Data = elements
            });
        }

        [HttpGet("{id}", Name = "GetHRSmsTemplateById")]
        public async Task<IActionResult> GetHRSmsTemplateById(long id)
        {
            var element = await _hrSmsTemplateRepository.GetByIdAsync(id);
            if (element == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid HRSmsTemplate Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HRSmsTemplateDto>
            {
                Success = true,
                Message = $"HRSmsTemplate is retrieved by Id: {id}",
                Data = element
            });
        }

        [HttpPost(Name = "CreateHRSmsTemplate")]
        public async Task<IActionResult> CreateHRSmsTemplate([FromBody] HRSmsTemplateDto HRSmsTemplateDto)
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


                if (await _hrSmsTemplateRepository.IsSmsTemplateExist(null,HRSmsTemplateDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "HRSmsTemplate name already exists."
                    });
                }

                var element = await _hrSmsTemplateRepository.CreateAsync(HRSmsTemplateDto);
                return Ok(new ApiResponseModel<HRSmsTemplateDto>
                {
                    Success = true,
                    Message = "HRSmsTemplate created successfully.",
                    Data = element
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating element.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.Message,
                    });
            }
        }

        [HttpPut("{id}", Name = "EditHRSmsTemplate")]
        public async Task<IActionResult> EditHRSmsTemplate(long id, [FromBody] HRSmsTemplateDto HRSmsTemplateDto)
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

                if (await _hrSmsTemplateRepository.IsSmsTemplateExist(id,HRSmsTemplateDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "HRSmsTemplate name already exists."
                    });
                }

                var element = await _hrSmsTemplateRepository.UpdateAsync(id,HRSmsTemplateDto);
                if (element == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"HRSmsTemplate with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRSmsTemplateDto>
                {
                    Success = true,
                    Message = $"HRSmsTemplate with ID {id} updated successfully.",
                    Data = element
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating element.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteHRSmsTemplate")]
        public async Task<IActionResult> DeleteHRSmsTemplate(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid HRSmsTemplate Id.",
                        Data = null
                    });
                }

                var deletedElement = await _hrSmsTemplateRepository.SoftDeleteAsyncs(id);

                if (deletedElement == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"HRSmsTemplate with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRSmsTemplateDto>
                {
                    Success = true,
                    Message = $"HRSmsTemplate with Id {id} deleted successfully.",
                    Data = deletedElement   
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting Element with Id {ElementId}.", id);

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