using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.models.plant;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly plantdbContext _context;

        public PlantsController(plantdbContext context)
        {
            _context = context;
        }

        // GET: api/Plants
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Plants>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }



        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlants([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plants = await _context.Plants.FindAsync(id);

            if (plants == null)
            {
                return NotFound();
            }

            return Ok(plants);
        }

        // PUT: api/Plants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlants([FromRoute] int id, [FromBody] Plants plants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plants.PlantId)
            {
                return BadRequest();
            }

            _context.Entry(plants).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Plants
        [HttpPost]
        public async Task<IActionResult> PostPlants([FromBody] Plants plants)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Plants.Add(plants);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlants", new { id = plants.PlantId }, plants);
        }

        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlants([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plants = await _context.Plants.FindAsync(id);
            if (plants == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plants);
            await _context.SaveChangesAsync();

            return Ok(plants);
        }

        private bool PlantsExists(int id)
        {
            return _context.Plants.Any(e => e.PlantId == id);
        }
    }
}