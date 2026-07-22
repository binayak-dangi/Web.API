using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models.Common;
using Web.API.Models.DTOS;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService,ILogger<AuthController> logger)
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

        return Ok(new ApiResponseModel<LoginResponseDto>
        {
            Success = true,
            Message = "Login successful.",
            Data = result
        });
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto request)
    {
        var result = await _authService.RefreshToken(request.Token);

        if (result == null)
        {
            return Unauthorized(new ApiResponseModel<object>
            {
                Success = false,
                Message = "Invalid or expired refresh token."
            });
        }

        return Ok(new ApiResponseModel<LoginResponseDto>
        {
            Success = true,
            Message = "Token refreshed successfully.",
            Data = result
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenRequestDto request)
    {
        var result = await _authService.LogoutAsync(request.Token);

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
}