using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixCharge.API.Models.InputModels;
using PixCharge.API.Persistence;
using PixCharge.API.Services;

namespace PixCharge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IAuthService _authService;
        public UserController(AppDbContext db, IAuthService authService)
        {
            _db = db;
            _authService = authService;
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

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {

            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] CreateUserInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid user data.");
            }

            model.Password = _authService.ComputeHash(model.Password);
            var user = model.toEntity();

            _db.Users.Add(user);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid login data.");
            }

            var user = _db.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null || user.PasswordHash != _authService.ComputeHash(model.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _authService.GenerateToken(user.Email, user.Role);
            return Ok(new { Token = token });
        }
    }
}