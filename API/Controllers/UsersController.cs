using ISP_Backend_Dotnet.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISP_Backend_Dotnet.API.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}/data")]
        public async Task<IActionResult> GetUserData(string userId)
        {
            try
            {
                var user = await _userService.GetUserDataAsync(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                if (ex.Message == "User not found")
                    return NotFound(new { message = "User not found" });

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPost("{userId}/usage")]
        public async Task<IActionResult> UpdateUsage(string userId, [FromBody] UpdateUsageRequest request)
        {
            try
            {
                var result = await _userService.UpdateUsageAsync(userId, request.Amount);

                if (result)
                    return Ok(new { status = "success" });
                else
                    return NotFound(new { message = "User not found" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        public class UpdateUsageRequest
        {
            public decimal Amount { get; set; }
        }
    }
}