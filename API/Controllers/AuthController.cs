using ISP_Backend_Dotnet.Application.DTOs;
using ISP_Backend_Dotnet.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISP_Backend_Dotnet.API.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _userService.LoginAsync(request.Email!, request.Password!);
                return Ok(user);
            }
            catch (Exception ex)
            {
                if (ex.Message == "User not found")
                    return Unauthorized(new { message = "Invalid email or password" });

                if (ex.Message == "Invalid password")
                    return Unauthorized(new { message = "Invalid password" });

                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}