using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PixCharge.API.Models.InputModels;
using PixCharge.API.Persistence;
using PixCharge.API.Services;

namespace PixCharge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IUserContextService _userContext;
        public ClientController(AppDbContext db, IUserContextService userContext)
        {
            _userContext = userContext;
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _db.Clients.ToList();
            if (result == null || !result.Any())
            {
                return NotFound("No clients found.");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var client = _db.Clients.FirstOrDefault(c => c.Id == id);
            if (client == null)
            {
                return NotFound($"Client with ID {id} not found.");
            }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateClientInputModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client data.");
            }

            var client = model.ToEntity(_userContext.UserId);
            _db.Clients.Add(client);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }
    }
}