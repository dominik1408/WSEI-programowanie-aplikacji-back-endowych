using CarSharingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarBrandsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarBrand>>> GetCarBrand()
        {
            return await _context.CarBrands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarBrand>> GetCarBrands(int id)
        {
            var carBrand = await _context.CarBrands.FindAsync(id);

            if (carBrand == null)
            {
                return NotFound();
            }

            return Ok(carBrand);
        }

        [HttpPost]
        public async Task<ActionResult<CarBrand>> PostCar(CarBrand carBrand)
        {
            var carBrandModel = new CarBrand
            {
                Name = carBrand.Name
            };

            _context.CarBrands.Add(carBrandModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarBrand),
                new {id = carBrandModel.CarBrandId},
                carBrand);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarBrand(int id)
        {
            var carBrand = await _context.CarBrands.FindAsync(id);

            if(carBrand == null)
            {
                return NotFound();
            }

            _context.CarBrands.Remove(carBrand);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarBrand(CarBrand carBrand, int id)
        {
            if(id != carBrand.CarBrandId)
            {
                return BadRequest();
            }

            var carBrandModel = await _context.CarBrands.FindAsync(id);
            if(carBrandModel == null)
            {
                return NotFound();
            }

            carBrandModel.Name = carBrand.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CarBrandExist(id))
            {

                return NotFound();
            }

            return Ok(carBrandModel);
        }

        public bool CarBrandExist(int id)
        {
            return _context.CarBrands.Any(a => a.CarBrandId == id);
        }
    }
}
