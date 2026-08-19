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
    public class HREmailTemplateController : ControllerBase
    {
        private readonly ILogger<HREmailTemplateController> _logger;
        private readonly IHREmailTemplateRepository _hrEmailTemplateRepository;

        public HREmailTemplateController(ILogger<HREmailTemplateController> logger, IHREmailTemplateRepository hrEmailTemplateRepository)
        {
            _logger = logger;
            _hrEmailTemplateRepository = hrEmailTemplateRepository;
        }

        [HttpGet(Name = "GetHREmailTemplates")]
        public async Task<IActionResult> GetHREmailTemplates([FromQuery] PaginationModel pagination)
        {
            var elements = await _hrEmailTemplateRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HREmailTemplateDto>>
            {
                Success = true,
                Message = "HREmailTemplates retrieved successfully.",
                Data = elements
            });
        }

        [HttpGet("{id}", Name = "GetHREmailTemplateById")]
        public async Task<IActionResult> GetHREmailTemplateById(long id)
        {
            var element = await _hrEmailTemplateRepository.GetByIdAsync(id);
            if (element == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid HREmailTemplate Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HREmailTemplateDto>
            {
                Success = true,
                Message = $"HREmailTemplate is retrieved by Id: {id}",
                Data = element
            });
        }

        [HttpPost(Name = "CreateHREmailTemplate")]
        public async Task<IActionResult> CreateHREmailTemplate([FromBody] HREmailTemplateDto HREmailTemplateDto)
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


                if (await _hrEmailTemplateRepository.IsEmailTemplateExist(null,HREmailTemplateDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "HREmailTemplate name already exists."
                    });
                }

                var element = await _hrEmailTemplateRepository.CreateAsync(HREmailTemplateDto);
                return Ok(new ApiResponseModel<HREmailTemplateDto>
                {
                    Success = true,
                    Message = "HREmailTemplate created successfully.",
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

        [HttpPut("{id}", Name = "EditHREmailTemplate")]
        public async Task<IActionResult> EditHREmailTemplate(long id, [FromBody] HREmailTemplateDto HREmailTemplateDto)
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

                if (await _hrEmailTemplateRepository.IsEmailTemplateExist(id,HREmailTemplateDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "HREmailTemplate name already exists."
                    });
                }

                var element = await _hrEmailTemplateRepository.UpdateAsync(id,HREmailTemplateDto);
                if (element == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"HREmailTemplate with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HREmailTemplateDto>
                {
                    Success = true,
                    Message = $"HREmailTemplate with ID {id} updated successfully.",
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

        [HttpDelete("{id}", Name = "DeleteHREmailTemplate")]
        public async Task<IActionResult> DeleteHREmailTemplate(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid HREmailTemplate Id.",
                        Data = null
                    });
                }

                var deletedElement = await _hrEmailTemplateRepository.SoftDeleteAsyncs(id);

                if (deletedElement == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"HREmailTemplate with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HREmailTemplateDto>
                {
                    Success = true,
                    Message = $"HREmailTemplate with Id {id} deleted successfully.",
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