using ExerciseLAB3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseLAB3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var users = _context.Users;
            return await users.ToListAsync();
        }

        //GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User don't exist");
            }
            return Ok(user);

        }

        //POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            var newUser = new User
            {
                name = user.name,
                email = user.email
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetUser), new {id = user.id},
            newUser);
        }
    }
}
