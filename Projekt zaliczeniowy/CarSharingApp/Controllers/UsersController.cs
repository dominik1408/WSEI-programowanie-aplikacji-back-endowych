using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarSharingApp.Data;
using CarSharingApp.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CarSharingApp.Controllers
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

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound("Entity Users is null.");
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound("Entity Users is null.");
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User does not exist.");
            }

            return user;
        }

        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByRole(string role)
        {
            if(_context.Users == null)
            {
                return NotFound("Entity Users is null.");
            }

            switch(role)
            {
                case "admin":
                    return await _context.Users.Where(a => a.Roles == Enums.Roles.Admin).ToListAsync();
                case "user":
                    return await _context.Users.Where(a => a.Roles == Enums.Roles.User).ToListAsync();
            }
            return NotFound("You probably entered the wrong path!");
        }

       //PATCH: api/users/id
       [HttpPatch("{id}")]
       public async Task<IActionResult> UpdateUsersPatch(int id, [FromBody] JsonPatchDocument<User> user)
       {
            var findUser = await _context.Users.FindAsync(id);
            try{
                user.ApplyTo(findUser);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound("User does not exist.");
            }
            return Ok(findUser);
       }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserPut(User user,int id)
        {
            if(id != user.UserId)
            {
                return BadRequest();
            }

            var userModel = await _context.Users.FindAsync(id);
            if(userModel == null)
            {
                return NotFound("User does not exist.");
            }

            userModel.Name = user.Name;
            userModel.Surname = user.Surname;
            userModel.Login = user.Login;
            userModel.Password = user.Password;
            userModel.Email = user.Email;
            userModel.PhoneNumber = user.PhoneNumber;
            userModel.Roles = user.Roles;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!UserExists(id))
            {
                return NotFound("User does not exist.");
            }

            return Ok(userModel);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity Users is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound("Entity Users is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User does not exist.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
