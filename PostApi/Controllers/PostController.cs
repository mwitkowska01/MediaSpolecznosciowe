using Microsoft.AspNetCore.Mvc;
using PostApi.Data;
using PostApi.Models;

namespace PostApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly PostDbContext _context;

        public PostsController(PostDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _context.Posts.ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest("Post data is missing.");
            }

            _context.Posts.Add(post);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = post.Id }, post);
        }
    }
}
