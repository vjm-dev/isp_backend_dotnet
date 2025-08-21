using ISP_Backend_Dotnet.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISP_Backend_Dotnet.API.Controllers
{
    [ApiController]
    [Route("v1/plans")]
    public class PlansController : ControllerBase
    {
        private readonly ISPContext _context;

        public PlansController(ISPContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            try
            {
                var plans = await _context.Plans.ToListAsync();
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}