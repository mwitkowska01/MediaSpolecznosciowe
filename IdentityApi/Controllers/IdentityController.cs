using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityDbContext _context;

        public IdentityController(IdentityDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing.");
            }

            var existingUser = _context.Users
                .SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

            if (existingUser == null)
            {
                return Unauthorized("Invalid UserName or password.");
            }

            return Ok(new { message = "Login successful", user = existingUser });
        }
    }
}
