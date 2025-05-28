
using Microsoft.AspNetCore.Mvc;

namespace PixCharge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("PixCharge API is running!");
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { status = "running", timestamp = DateTime.UtcNow });
        }
    }
}