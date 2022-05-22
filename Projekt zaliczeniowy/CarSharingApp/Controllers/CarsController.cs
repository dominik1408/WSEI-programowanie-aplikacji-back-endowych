using CarSharingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }
        
        //GET: api/cars/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if(car == null)
            {
                return NotFound("Not found this car");
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            var carModel = new Car
            {
                RegistrationNumber = car.RegistrationNumber,
                MeterStatus = car.MeterStatus,
                ProductionYear = car.ProductionYear,
                IsActive = car.IsActive,
                CarBrandId = car.CarBrandId,
                CarModelId = car.CarModelId,
                ColorId = car.ColorId
            };

            _context.Cars.Add(carModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCars), new {id = carModel.CarId});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if(car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
         }

        [HttpPut]
        public async Task<IActionResult> PutCar(Car car ,int id)
        {
            if( id != car.CarId)
            {
                return BadRequest();
            }

            var carModel = await _context.Cars.FindAsync(id);
            if(carModel == null)
            {
                return NotFound();
            }

            carModel.RegistrationNumber = car.RegistrationNumber;
            carModel.MeterStatus = car.MeterStatus;
            carModel.ProductionYear = car.ProductionYear;
            carModel.IsActive = car.IsActive;
            carModel.CarBrandId = car.CarBrandId;
            carModel.CarModelId = car.CarModelId;
            carModel.ColorId = car.ColorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!CarExists(id))
            {
                return NotFound();
            }

            return Ok(carModel);

        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(a => a.CarId == id);
        }

    }
}
