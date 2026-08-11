using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models.Common;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;

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

    //[HttpPost("login")]
    //[AllowAnonymous]
    //public async Task<IActionResult> Login(LoginRequestDto request)
    //{
    //    var result = await _authService.LoginAsync(request);

    //    if (result == null)
    //    {
    //        return Unauthorized(new ApiResponseModel<object>
    //        {
    //            Success = false,
    //            Message = "Invalid username or password."
    //        });
    //    }

    //    return Ok(new ApiResponseModel<LoginResponseDto>
    //    {
    //        Success = true,
    //        Message = "Login successful.",
    //        Data = result
    //    });
    //}


    //[HttpPost("refresh-token")]
    //[AllowAnonymous]
    //public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
    //{
    //    var result = await _authService.RefreshToken(request.Token);

    //    if (result == null)
    //    {
    //        return Unauthorized(new ApiResponseModel<object>
    //        {
    //            Success = false,
    //            Message = "Invalid or expired refresh token."
    //        });
    //    }

    //    return Ok(new ApiResponseModel<LoginResponseDto>
    //    {
    //        Success = true,
    //        Message = "Token refreshed successfully.",
    //        Data = result
    //    });
    //}

    //[HttpPost("logout")]
    //public async Task<IActionResult> Logout(RefreshTokenRequestDto request)
    //{
    //    var result = await _authService.LogoutAsync(request.Token);

    //    if (!result)
    //    {
    //        return BadRequest(new ApiResponseModel<object>
    //        {
    //            Success = false,
    //            Message = "Invalid refresh token."
    //        });
    //    }

    //    return Ok(new ApiResponseModel<object>
    //    {
    //        Success = true,
    //        Message = "Logout successful."
    //    });
    //}
}