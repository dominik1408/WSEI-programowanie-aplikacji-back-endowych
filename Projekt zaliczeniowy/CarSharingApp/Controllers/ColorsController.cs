using CarSharingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ColorsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(int id)
        {
            var color = await _context.Colors.FindAsync(id);

            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }

        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
            var colorModel = new Color
            {
                ColorName = color.ColorName
            };

            _context.Colors.Add(colorModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColors),
                new { id = colorModel.ColorId },
                color);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _context.Colors.FindAsync(id);
            if(color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(Color color, int id)
        {
            if(id != color.ColorId)
            {
                return BadRequest();
            }

            var colorModel = _context.Colors.FindAsync(id);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!ColorExist(id))
            {
                return NotFound();
            }

            return Ok(colorModel);

        }

        public bool ColorExist(int id)
        {
            return _context.Colors.Any(a => a.ColorId == id);
        }
    }
}
