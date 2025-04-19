using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrebuildPatternController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrebuildPatternController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrebuildPattern
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrebuildPattern>>> GetPrebuildPatterns()
        {
            return await _context.PrebuildPatterns.ToListAsync();
        }

        // GET: api/PrebuildPattern/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrebuildPattern>> GetPrebuildPattern(int id)
        {
            var prebuildPattern = await _context.PrebuildPatterns.FindAsync(id);

            if (prebuildPattern == null)
            {
                return NotFound();
            }

            return prebuildPattern;
        }

        // PUT: api/PrebuildPattern/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrebuildPattern(int id, PrebuildPattern prebuildPattern)
        {
            if (id != prebuildPattern.SerialNumber)
            {
                return BadRequest();
            }

            _context.Entry(prebuildPattern).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrebuildPatternExists(id))
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

        // POST: api/PrebuildPattern
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrebuildPattern>> PostPrebuildPattern(PrebuildPattern prebuildPattern)
        {
            _context.PrebuildPatterns.Add(prebuildPattern);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrebuildPattern", new { id = prebuildPattern.SerialNumber }, prebuildPattern);
        }

        // DELETE: api/PrebuildPattern/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrebuildPattern(int id)
        {
            var prebuildPattern = await _context.PrebuildPatterns.FindAsync(id);
            if (prebuildPattern == null)
            {
                return NotFound();
            }

            _context.PrebuildPatterns.Remove(prebuildPattern);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrebuildPatternExists(int id)
        {
            return _context.PrebuildPatterns.Any(e => e.SerialNumber == id);
        }
    }
}
