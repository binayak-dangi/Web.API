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
    public class HRBranchController : ControllerBase
    {
        private readonly ILogger<HRBranchController> _logger;
        private readonly IHRBranchService _hrBranchService;

        public HRBranchController(
            ILogger<HRBranchController> logger,
            IHRBranchService hrBranchService)
        {
            _logger = logger;
            _hrBranchService = hrBranchService;
        }

        [HttpGet(Name = "GetBranches")]
        public async Task<IActionResult> GetBranches()
        {
            var branches = await _hrBranchService.GetAllBranchesAsync();

            return Ok(new ApiResponseModel<List<HRBranchDto>>
            {
                Success = true,
                Message = "Branches retrieved successfully.",
                Data = branches
            });
        }

        [HttpGet("{id}", Name = "GetBranchById")]
        public async Task<IActionResult> GetBranchById(long id)
        {
            var branch = await _hrBranchService.GetBranchByIdAsync(id);
            if (branch == null)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = $"Invalid Branch Id: {id}",
                    Data = { }
                });
            }

            return Ok(new ApiResponseModel<HRBranchDto>
            {
                Success = true,
                Message = $"Branch is retrieved by Id: {id}",
                Data = branch
            });
        }

        [HttpPost(Name = "CreateBranch")]
        public async Task<IActionResult> CreateBranch([FromBody] HRBranchDto branchDto)
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



                var branch = await _hrBranchService.CreateBranchAsync(branchDto);

                return Ok(new ApiResponseModel<HRBranchDto>
                {
                    Success = true,
                    Message = "Branch created successfully.",
                    Data = branch
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
                        Data = null
                    });
            }
        }

        [HttpPut("{id}", Name = "EditBranch")]
        public async Task<IActionResult> EditBranch(long id, [FromBody] HRBranchDto branchDto)
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

                branchDto.Id = id;   // Set the ID from the route

                var branch = await _hrBranchService.UpdateBranchAsync(branchDto);

                if (branch == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Branch with ID {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRBranchDto>
                {
                    Success = true,
                    Message = $"Branch with ID {id} updated successfully.",
                    Data = branch
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role.");

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "An unexpected error occurred.",
                        Data = null
                    });
            }
        }

        [HttpDelete("{id}", Name = "DeleteBranch")]
        public async Task<IActionResult> DeleteBranch(long id)
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

                var deletedBranch = await _hrBranchService.DeleteBranchAsync(id);

                if (deletedBranch == null)
                {
                    return NotFound(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = $"Branch with Id {id} not found.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<HRBranchDto>
                {
                    Success = true,
                    Message = $"Branch with Id {id} deleted successfully.",
                    Data = deletedBranch
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting branch with Id {BranchId}.", id);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "An unexpected error occurred.",
                        Data = null
                    });
            }
        }
    }
}