using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Repositories.Setup.Interfaces;

namespace Web.API.Controllers.Setup
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HRBranchController : ControllerBase
    {
        private readonly ILogger<HRBranchController> _logger;
        private readonly IHRBranchRepository _hrBranchService;

        public HRBranchController(ILogger<HRBranchController> logger, IHRBranchRepository hrBranchService)
        {
            _logger = logger;
            _hrBranchService = hrBranchService;
        }

        [HttpGet(Name = "GetBranches")]
        public async Task<IActionResult> GetBranches([FromQuery] PaginationModel pagination)
        {
            var branches = await _hrBranchService.GetAllAsync(pagination);

            return Ok(new ApiResponseModel<PagedResult<HRBranchDto>>
            {
                Success = true,
                Message = "Branches retrieved successfully.",
                Data = branches
            });
        }

        [HttpGet("{id}", Name = "GetBranchById")]
        public async Task<IActionResult> GetBranchById(long id)
        {
            var branch = await _hrBranchService.GetByIdAsync(id);
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


                if (await _hrBranchService.IsBranchExist(null,branchDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Branch name already exists."
                    });
                }

                var branch = await _hrBranchService.CreateAsync(branchDto);

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

                if (await _hrBranchService.IsBranchExist(id,branchDto))
                {
                    return Conflict(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Branch name already exists."
                    });
                }

                var branch = await _hrBranchService.UpdateAsync(id,branchDto);

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
                        Message = ex.InnerException?.Message ?? ex.Message,
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

                var deletedBranch = await _hrBranchService.SoftDeleteAsyncs(id);

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
                        Message = ex.InnerException?.Message ?? ex.Message,
                        Data = null
                    });
            }
        }
    }
}