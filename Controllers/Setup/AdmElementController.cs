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
    public class AdmElementController : ControllerBase
    {
        private readonly ILogger<AdmElementController> _logger;
        private readonly IAdmElementRepository _admElementRepository;

        public AdmElementController(ILogger<AdmElementController> logger, IAdmElementRepository admElementRepository)
        {
            _logger = logger;
            _admElementRepository = admElementRepository;
        }

        [HttpGet(Name = "GetAdmElements")]
        public async Task<IActionResult> GetAdmElements([FromQuery] PaginationModel pagination)
        {
            var elements = await _admElementRepository.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<AdmElementDto>>
            {
                Success = true,
                Message = "Elements retrieved successfully.",
                Data = elements
            });
        }

        [HttpGet("{id}", Name = "GetAdmElementById")]
        public async Task<IActionResult> GetAdmElementById(long id)
        {
            var element = await _admElementRepository.GetByIdAsync(id);
            if (element == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Element Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<AdmElementDto>
            {
                Success = true,
                Message = $"Element is retrieved by Id: {id}",
                Data = element
            });
        }

        [HttpPost(Name = "CreateAdmElement")]
        public async Task<IActionResult> CreateAdmElement([FromBody] AdmElementDto ElementDto)
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


                if (await _admElementRepository.IsElementExist(null,ElementDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Element name already exists."
                    });
                }

                var element = await _admElementRepository.CreateAsync(ElementDto);
                return Ok(new ApiResponseModel<AdmElementDto>
                {
                    Success = true,
                    Message = "Element created successfully.",
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

        [HttpPut("{id}", Name = "EditAdmElement")]
        public async Task<IActionResult> EditAdmElement(long id, [FromBody] AdmElementDto ElementDto)
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

                if (await _admElementRepository.IsElementExist(id,ElementDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Element name already exists."
                    });
                }

                var element = await _admElementRepository.UpdateAsync(id,ElementDto);
                if (element == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Element with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<AdmElementDto>
                {
                    Success = true,
                    Message = $"Element with ID {id} updated successfully.",
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

        [HttpDelete("{id}", Name = "DeleteAdmElement")]
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

                var deletedElement = await _admElementRepository.SoftDeleteAsyncs(id);

                if (deletedElement == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Element with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<AdmElementDto>
                {
                    Success = true,
                    Message = $"Element with Id {id} deleted successfully.",
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