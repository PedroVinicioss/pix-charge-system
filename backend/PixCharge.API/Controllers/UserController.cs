using Microsoft.AspNetCore.Mvc;
using PixCharge.API.Persistence;

namespace PixCharge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UserController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _db.Users.ToList();
            if (result == null || !result.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(result);
        }
    }
}