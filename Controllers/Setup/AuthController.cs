using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;
using Web.API.Repositories.Setup.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthRepository authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);

        if (result == null)
        {
            return Unauthorized(new ApiResponseModel<object>
            {
                Success = false,
                Message = "Invalid username or password."
            });
        }

        if (result.Employee.isNewlyAdded)
        {
            return Ok(new ApiResponseModel<object>
            {
                Success = true,
                Message = "Please change your password.",
                Data = new
                {
                    Id = result.Employee.Id,
                    Username = result.Employee.Username,
                    IsNewlyAdded = true
                }
            });
        }

        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,               // false only during local HTTP development
            SameSite = SameSiteMode.Lax,
            Expires = result.ExpiresAt
        });

        // Don't send RefreshToken back to React
        result.RefreshToken = null;

        return Ok(new ApiResponseModel<LoginResponseDto>
        {
            Success = true,
            Message = "Login successful.",
            Data = result
        });
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized();
        }

        var result = await _authService.RefreshToken(refreshToken);

        if (result == null)
        {
            return Unauthorized(new ApiResponseModel<object>
            {
                Success = false,
                Message = "Invalid or expired refresh token."
            });
        }

        Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = false,
            SameSite = SameSiteMode.Lax,
            Expires = result.ExpiresAt
        });

        result.RefreshToken = null;

        return Ok(new ApiResponseModel<LoginResponseDto>
        {
            Success = true,
            Message = "Token refreshed successfully.",
            Data = result
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest();
        }

        var result = await _authService.LogoutAsync(refreshToken);

        Response.Cookies.Delete("refreshToken");

        if (!result)
        {
            return BadRequest(new ApiResponseModel<object>
            {
                Success = false,
                Message = "Invalid refresh token."
            });
        }

        return Ok(new ApiResponseModel<object>
        {
            Success = true,
            Message = "Logout successful."
        });
    }

   
    [HttpPost("complete-first-password")]
    [AllowAnonymous]
    public async Task<IActionResult> CompleteFirstPassword(
    [FromBody] CompleteFirstPasswordDto dto)
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

            var result = await _authService.CompleteFirstPasswordAsync(dto);

            if (result == PasswordSetupResult.EmployeeNotFound ||
                result == PasswordSetupResult.InvalidCurrentPassword)
            {
                return Unauthorized(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    Data = null
                });
            }

            if (result == PasswordSetupResult.NotNewlyAdded)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "This user is not a newly added user. Please use the password reset option.",
                    Data = null
                });
            }

            if (result == PasswordSetupResult.PasswordMismatch)
            {
                return BadRequest(new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "New password and confirm password do not match.",
                    Data = null
                });
            }

            if (result == PasswordSetupResult.Success)
            {
                return Ok(new ApiResponseModel<object>
                {
                    Success = true,
                    Message = "Password changed successfully. Please login with your new password.",
                    Data = null
                });
            }

            return BadRequest(new ApiResponseModel<object>
            {
                Success = false,
                Message = "Unable to change password.",
                Data = null
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred while completing first password setup for {Username}.",
                dto.Username);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ApiResponseModel<object>
                {
                    Success = false,
                    Message = "An error occurred while changing the password.",
                    Data = null
                });
        }
    }

   
}