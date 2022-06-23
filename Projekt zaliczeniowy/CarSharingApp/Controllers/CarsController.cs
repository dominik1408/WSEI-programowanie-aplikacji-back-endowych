
using CarSharingApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarSharingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : Controller
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/cars
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var cars = (from c in _context.Cars
                        join cb in _context.CarBrands on c.CarBrandId equals cb.CarBrandId
                        join cm in _context.CarModels on c.CarModelId equals cm.CarModelId
                        join color in _context.Colors on c.ColorId equals color.ColorId
                        select new 
                        {
                            CarId = c.CarId,
                            RegistrationNumber = c.RegistrationNumber,
                            MeterStatus = c.MeterStatus,
                            ProductionYear = c.ProductionYear,
                            IsActive = c.IsActive,
                            CarBrand = cb.Name,
                            CarModel = cm.Name,
                            Color = color.ColorName
                        }).ToListAsync();
            return Json(await cars);
        }
        
        //GET: api/cars/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if(car == null)
            {
                return NotFound($"Car with id {id} does not exist");
            }

            var carDetails = (from c in _context.Cars
                        join cb in _context.CarBrands on c.CarBrandId equals cb.CarBrandId
                        join cm in _context.CarModels on c.CarModelId equals cm.CarModelId
                        join color in _context.Colors on c.ColorId equals color.ColorId
                        where car.CarId == c.CarId
                        select new
                        {
                            CarId = c.CarId,
                            RegistrationNumber = c.RegistrationNumber,
                            MeterStatus = c.MeterStatus,
                            ProductionYear = c.ProductionYear,
                            IsActive = c.IsActive,
                            CarBrand = cb.Name,
                            CarModel = cm.Name,
                            Color = color.ColorName
                        }).ToListAsync();

            return Json(await carDetails);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<Car>>> GetCarsByBrand(string brandName)
        {
            var carBrands = (from c in _context.Cars
                             join cb in _context.CarBrands on c.CarBrandId equals cb.CarBrandId
                             join cm in _context.CarModels on c.CarModelId equals cm.CarModelId
                             join color in _context.Colors on c.ColorId equals color.ColorId
                             where cb.Name.Contains(brandName)
                             select new
                             {
                                 CarId = c.CarId,
                                 RegistrationNumber = c.RegistrationNumber,
                                 MeterStatus = c.MeterStatus,
                                 ProductionYear = c.ProductionYear,
                                 IsActive = c.IsActive,
                                 CarBrand = cb.Name,
                                 CarModel = cm.Name,
                                 Color = color.ColorName
                
                        }).ToListAsync();
  

                return Json(await carBrands);
        }

        [HttpGet("sort")]
        public async Task <ActionResult<IEnumerable<Car>>> SortCarsByProductionYear(string orderBy)
        {
            var car = (from c in _context.Cars
                       join cb in _context.CarBrands on c.CarBrandId equals cb.CarBrandId
                       join cm in _context.CarModels on c.CarModelId equals cm.CarModelId
                       join color in _context.Colors on c.ColorId equals color.ColorId
                       select new
                       {
                           CarId = c.CarId,
                           RegistrationNumber = c.RegistrationNumber,
                           MeterStatus = c.MeterStatus,
                           ProductionYear = c.ProductionYear,
                           IsActive = c.IsActive,
                           CarBrand = cb.Name,
                           CarModel = cm.Name,
                           Color = color.ColorName

                       });
            switch(orderBy)
            {
                case "productionYearDESC":
                    return Json(await car.OrderByDescending(a => a.ProductionYear).ToListAsync());
                case "productionYearASC":
                    return Json(await car.OrderBy(a => a.ProductionYear).ToListAsync());
                case "meterStatusDESC":
                    return Json(await car.OrderByDescending(a => a.MeterStatus).ToListAsync());
                case "meterStatusASC":
                    return Json(await car.OrderBy(a => a.MeterStatus).ToListAsync());
            }

            return Json(await car.ToListAsync());
        }

        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Car>>> FilterCasrByStatus(string isActive)
        {
            var car = (from c in _context.Cars
                       join cb in _context.CarBrands on c.CarBrandId equals cb.CarBrandId
                       join cm in _context.CarModels on c.CarModelId equals cm.CarModelId
                       join color in _context.Colors on c.ColorId equals color.ColorId
                       select new
                       {
                           CarId = c.CarId,
                           RegistrationNumber = c.RegistrationNumber,
                           MeterStatus = c.MeterStatus,
                           ProductionYear = c.ProductionYear,
                           IsActive = c.IsActive,
                           CarBrand = cb.Name,
                           CarModel = cm.Name,
                           Color = color.ColorName

                       });

            switch (isActive)
            {
                case "true":
                    return Json(await car.Where(a => a.IsActive == true).ToListAsync());
                case "false":
                    return Json(await car.Where(a => a.IsActive == false).ToListAsync());
            }

            return Json(await car.ToListAsync());
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

            return CreatedAtAction(nameof(GetCars), 
                new {id = carModel.CarId},
                car);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if(car == null)
            {
                return NotFound("Car does not exist.");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
         }
        
        //PATCH: api/cars/id
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCarsPatch(int id, [FromBody] JsonPatchDocument<Car> car)
        {
            var findCar = await _context.Cars.FindAsync(id);
            try{
                car.ApplyTo(findCar);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!CarExists(id))
            {
                return NotFound("Car does not exist.");
            }
            return Ok(findCar);
        }

        //PUT: api/cars/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCars(Car car, int id)
        {
            if(id != car.CarId)
            {
                return BadRequest();
            }

            var carModel = await _context.Cars.FindAsync(id);
            if(carModel == null)
            {
                return NotFound("Car does not exist.");
            }

            carModel.RegistrationNumber = car.RegistrationNumber;
            carModel.MeterStatus = car.MeterStatus;
            carModel.ProductionYear = car.ProductionYear;
            carModel.IsActive = car.IsActive;
            carModel.CarBrand = car.CarBrand;
            carModel.CarModel = car.CarModel;
            carModel.Color = car.Color;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) when (!CarExists(id))
            {
                return NotFound("Car does not exist.");
            }

            return Ok(carModel);
        }

        public bool CarExists(int id)
        {
            return _context.Cars.Any(a => a.CarId == id);
        }

    }
}
