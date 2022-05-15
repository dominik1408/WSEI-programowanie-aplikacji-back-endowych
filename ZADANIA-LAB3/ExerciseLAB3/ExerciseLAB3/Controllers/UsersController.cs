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
                return NotFound("User doesn't exist");
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
            nameof(GetUser), new { id = user.id },
            newUser);
        }

        //PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest("User doesn't exist");
            }

            var someUser = await _context.Users.FindAsync(id);
            if (someUser == null)
            {
                return NotFound("User doesn't exist"); ;
            }

            someUser.name = user.name;
            someUser.email = user.email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExist(id))
            {
                return NotFound("User doesn't exist");
            }

            return Ok(someUser);
        }

        //DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound("User doesn't exist");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExist(int id)
        {
            return _context.Users.Any(a => a.id == id);
        }
    }
}
