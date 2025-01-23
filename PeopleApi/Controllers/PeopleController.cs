using Microsoft.AspNetCore.Mvc;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace PeopleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public PeopleController(PeopleDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<People> Get()
        {
            return _context.People.ToList();
        }

 
        [HttpPost]
        public IActionResult Create([FromBody] People people)
        {
            if (people == null)
            {
                return BadRequest("People data is missing.");
            }

            _context.People.Add(people);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Create), new { id = people.Id }, people);
        }
    }
}
