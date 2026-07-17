using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.API.Data;
using Web.API.Models.Common;
using Web.API.Models.DTOS;
using Web.API.Services.Interface;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly AppDbContext _context;

        public AuthController(
            ILogger<AuthController> logger,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService,
           AppDbContext context)
        {
            _logger = logger;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid request payload layout.",
                        Data = null
                    });
                }

                // Locate employee matching structural parameters
                var employee = await _context.Set<MyTestMvc.Models.Setup.HREmployee>()
                    .FirstOrDefaultAsync(x => x.Username == request.Username && x.PasswordHash == request.Password);

                if (employee == null)
                {
                    return Unauthorized(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid username or password.",
                        Data = null
                    });
                }

                var accessToken = _jwtService.GenerateAccessToken(employee);
                var refreshToken = await _refreshTokenService.CreateToken(employee.Id);

                var responseData = new LoginResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                    ExpiresAt = refreshToken.Expires,
                    Employee = new HREmployeeDto
                    {
                        Id = employee.Id,
                        Username = employee.Username,
                        Email = employee.Email ?? string.Empty
                        // Explicitly map remaining structural criteria properties here
                    }
                };

                return Ok(new ApiResponseModel<LoginResponseDto>
                {
                    Success = true,
                    Message = "Login successful.",
                    Data = responseData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login sequence execution failed.");

                return StatusCode(500, new ApiResponseModel<object>
                {
                    Success = false,
                    Message = ex.GetBaseException().Message,
                    Data = null
                });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            try
            {
                var currentToken = await _refreshTokenService.GetToken(request.Token);

                if (currentToken == null || currentToken.IsRevoked || DateTime.UtcNow > currentToken.Expires)
                {
                    return Unauthorized(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid, expired, or revoked refresh token structural validation signature.",
                        Data = null
                    });
                }

                // Process internal rotation logic state mapping entries safely
                var updatedTokenRecord = await _refreshTokenService.RotateToken(request.Token);
                var newAccessToken = _jwtService.GenerateAccessToken(currentToken.Employee);

                var responseData = new LoginResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = updatedTokenRecord.Token,
                    ExpiresAt = updatedTokenRecord.Expires,
                    Employee = new HREmployeeDto
                    {
                        Id = currentToken.Employee.Id,
                        Username = currentToken.Employee.Username,
                        Email = currentToken.Employee.Email ?? string.Empty
                    }
                };

                return Ok(new ApiResponseModel<LoginResponseDto>
                {
                    Success = true,
                    Message = "Token refreshed successfully.",
                    Data = responseData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Refresh token verification criteria execution failed.");

                return StatusCode(500, new ApiResponseModel<object>
                {
                    Success = false,
                    Message = ex.GetBaseException().Message,
                    Data = null
                });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
        {
            try
            {
                var result = await _refreshTokenService.RevokeToken(request.Token);

                if (!result)
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Success = false,
                        Message = "Invalid token signature or unable to execute processing requirements.",
                        Data = null
                    });
                }

                return Ok(new ApiResponseModel<object>
                {
                    Success = true,
                    Message = "Logout successful.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Session logout verification handling framework execution failed.");

                return StatusCode(500, new ApiResponseModel<object>
                {
                    Success = false,
                    Message = ex.GetBaseException().Message,
                    Data = null
                });
            }
        }
    }
}
