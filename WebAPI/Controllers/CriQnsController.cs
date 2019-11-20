using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Cri;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriQnsController : ControllerBase
    {
        private readonly criTestQnsContext _context;

        public CriQnsController(criTestQnsContext context)
        {
            _context = context;
        }

        // GET: api/CriQns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriQn>>> GetCriQn()
        {
            return await _context.CriQn.ToListAsync();
        }

        [HttpGet]
        [Route("Tiers")]
        public async Task<ActionResult<IEnumerable<Tier>>> GetTiers()
        {
            return await _context.Tier.ToListAsync();
        }

        [HttpGet]
        [Route("Countries")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Country.ToListAsync();
        }

     
        [HttpGet]
        [Route("criqns/tier/{tierId}/country/{countryId}")]
        public async Task<ActionResult<IEnumerable<CriQn>>> GetCriQn(int? tierId, int? countryId)
        {
            var root = (IQueryable<CriQn>)_context.CriQn;
            if(tierId != -1)
            {
                root = root.Where(q=>q.TierFk == tierId);
            }
            if (countryId != -1)
            {
                root = root.Where(q => q.CountryFk == countryId);
            }
            return await root.ToListAsync();
        }


        // GET: api/CriQns/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCriQn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var criQn = await _context.CriQn.FindAsync(id);

            if (criQn == null)
            {
                return NotFound();
            }

            return Ok(criQn);
        }

        // PUT: api/CriQns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriQn([FromRoute] int id, [FromBody] CriQn criQn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != criQn.QnId)
            {
                return BadRequest();
            }

            _context.Entry(criQn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriQnExists(id))
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

        // POST: api/CriQns
        [HttpPost]
        public async Task<IActionResult> PostCriQn([FromBody] CriQn criQn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CriQn.Add(criQn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCriQn", new { id = criQn.QnId }, criQn);
        }

        // DELETE: api/CriQns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriQn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var criQn = await _context.CriQn.FindAsync(id);
            if (criQn == null)
            {
                return NotFound();
            }

            _context.CriQn.Remove(criQn);
            await _context.SaveChangesAsync();

            return Ok(criQn);
        }

        private bool CriQnExists(int id)
        {
            return _context.CriQn.Any(e => e.QnId == id);
        }
    }
}