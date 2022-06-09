using CarSharingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmin()
        {
            return await _context.Admins.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdminById(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if(admin == null)
            {
                return NotFound();
            }
            return Ok(admin);

        }

        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            var adminModel = new Admin
            {
                UserId = admin.UserId
            };

            _context.Admins.Add(adminModel);
            await _context.SaveChangesAsync();

           return CreatedAtAction(nameof(GetAdmin),
                new { id = adminModel.AdminId },
                admin);
        }

        
        [HttpPut("id")]
        public async Task<ActionResult<Admin>> PutAdmin(int id, Admin admin)
        {

            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            var adminModel = await _context.Admins.FindAsync(id);
            if (adminModel == null)
            {
                return NotFound();
            }

            adminModel.UserId = admin.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AdminExists(id))
            {

                return NotFound();
            }

            return Ok(adminModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }

    }
}
