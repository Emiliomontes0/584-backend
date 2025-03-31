using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modelDB;
using CarInventory.Dtos;

namespace CarInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelsController(SourceContext context) : ControllerBase
    {
        private readonly SourceContext _context = context;

        // GET: api/CarModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModel>>> GetCarModels()
        {
            return await _context.CarModels
                .Include(c => c.Make)
                .Take(100)
                .ToListAsync();
        }

        // GET: api/CarModels/modelmake
        [HttpGet("modelmake")]
        public async Task<ActionResult<IEnumerable<MakeModelDto>>> GetCarModelsWithMake()
        {
            return await _context.CarModels
                .Include(c => c.Make)
                .Select(model => new MakeModelDto
                {
                    Id = model.Id,
                    ModelName = model.Name,
                    Year = model.Year,
                    Engine = model.Engine,
                    Transmission = model.Transmission,
                    BodyStyle = model.BodyStyle,
                    Price = model.Price,
                    MakeName = model.Make.Name
                })
                .ToListAsync();
        }

        // GET: api/CarModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarModel>> GetCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);

            if (carModel == null)
            {
                return NotFound();
            }   
            return carModel;
        }


        // PUT: api/CarModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarModel(int id, CarModel carModel)
        {
            if (id != carModel.Id)
                return BadRequest();

            _context.Entry(carModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CarModels.Any(cm => cm.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/CarModels
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
        {
            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarModel), new { id = carModel.Id }, carModel);
        }

        // DELETE: api/CarModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel is null)
                return NotFound();

            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
