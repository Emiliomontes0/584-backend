using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modelDB;
using CarInventory.Dtos;

namespace CarInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController(SourceContext context) : ControllerBase
    {
        private readonly SourceContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            return await _context.Makes.ToListAsync();
        }

        [HttpGet("makestats")]
        public async Task<ActionResult<IEnumerable<MakeStatsDto>>> GetMakeStats()
        {
        return await _context.Makes
            .Select(make => new MakeStatsDto
            {
                MakeName = make.Name,
                Country = make.Country,
                FoundedYear = make.FoundedYear.GetValueOrDefault(),
                NumberOfModels = make.CarModels.Count,
                AveragePrice = make.CarModels.Average(m => (decimal?)m.Price).Value
            })
            .ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Make>> GetMake(int id)
        {
             var make = await _context.Makes.FindAsync(id);

            if (make == null)
            {
                return NotFound();
            }

            return make;
        }


        [HttpPost]
        public async Task<ActionResult<Make>> PostMake(Make make)
        {
            _context.Makes.Add(make);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMake), new { id = make.Id }, make);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id, Make make)
        {
            if (id != make.Id)
                return BadRequest();

            _context.Entry(make).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Makes.Any(m => m.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var make = await _context.Makes.FindAsync(id);
            if (make is null)
                return NotFound();

            _context.Makes.Remove(make);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
