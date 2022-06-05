using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using finalproject.Data;
using finalproject.Models;

namespace finalproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopsController : ControllerBase
    {
        private readonly finalprojectContext _context;

        public PopsController(finalprojectContext context)
        {
            _context = context;
        }

        // GET: api/Pops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pop>>> GetPops()
        {
          if (_context.Pops == null)
          {
              return NotFound();
          }
            return await _context.Pops.ToListAsync();
        }

        // GET: api/Pops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pop>> GetPop(int id)
        {
          if (_context.Pops == null)
          {
              return NotFound();
          }
            var pop = await _context.Pops.FindAsync(id);

            if (pop == null)
            {
                return NotFound();
            }

            return pop;
        }

        // PUT: api/Pops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPop(int id, Pop pop)
        {
            if (id != pop.year)
            {
                return BadRequest();
            }

            _context.Entry(pop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopExists(id))
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

        // POST: api/Pops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pop>> PostPop(Pop pop)
        {
          if (_context.Pops == null)
          {
              return Problem("Entity set 'finalprojectContext.Pops'  is null.");
          }
            _context.Pops.Add(pop);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PopExists(pop.year))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPop", new { id = pop.year }, pop);
        }

        // DELETE: api/Pops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePop(int id)
        {
            if (_context.Pops == null)
            {
                return NotFound();
            }
            var pop = await _context.Pops.FindAsync(id);
            if (pop == null)
            {
                return NotFound();
            }

            _context.Pops.Remove(pop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PopExists(int id)
        {
            return (_context.Pops?.Any(e => e.year == id)).GetValueOrDefault();
        }
    }
}
