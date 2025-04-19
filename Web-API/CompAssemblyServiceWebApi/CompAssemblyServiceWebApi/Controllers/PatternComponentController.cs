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
    public class PatternComponentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatternComponentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PatternComponent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatternComponent>>> GetPatternComponents()
        {
            return await _context.PatternComponents.ToListAsync();
        }

        // GET: api/PatternComponent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatternComponent>> GetPatternComponent(int id)
        {
            var patternComponent = await _context.PatternComponents.FindAsync(id);

            if (patternComponent == null)
            {
                return NotFound();
            }

            return patternComponent;
        }

        // PUT: api/PatternComponent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatternComponent(int id, PatternComponent patternComponent)
        {
            if (id != patternComponent.PatternComponentId)
            {
                return BadRequest();
            }

            _context.Entry(patternComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatternComponentExists(id))
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

        // POST: api/PatternComponent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatternComponent>> PostPatternComponent(PatternComponent patternComponent)
        {
            _context.PatternComponents.Add(patternComponent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatternComponent", new { id = patternComponent.PatternComponentId }, patternComponent);
        }

        // DELETE: api/PatternComponent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatternComponent(int id)
        {
            var patternComponent = await _context.PatternComponents.FindAsync(id);
            if (patternComponent == null)
            {
                return NotFound();
            }

            _context.PatternComponents.Remove(patternComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatternComponentExists(int id)
        {
            return _context.PatternComponents.Any(e => e.PatternComponentId == id);
        }
    }
}
