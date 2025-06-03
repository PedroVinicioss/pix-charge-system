using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixCharge.API.Models.InputModels;
using PixCharge.API.Services;

namespace PixCharge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAll();
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetById(id);
            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserInputModel model)
        {
            var result = await _userService.Register(model);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(Get), new { id = result.Data?.Id }, result);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginInputModel model)
        {
            var result = await _userService.Login(model);
            if (!result.IsSuccess)
                return Unauthorized(result.Message);
            
            return Ok(result);
        }
    }
}