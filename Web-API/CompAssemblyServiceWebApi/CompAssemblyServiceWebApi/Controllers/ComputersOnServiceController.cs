using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputersOnServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComputersOnServiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ComputersOnService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputerOnService>>> GetComputersOnService()
        {
            return await _context.ComputersOnService.ToListAsync();
        }

        // GET: api/ComputersOnService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComputerOnService>> GetComputerOnService(int id)
        {
            var computerOnService = await _context.ComputersOnService.FindAsync(id);

            if (computerOnService == null)
            {
                return NotFound();
            }

            return computerOnService;
        }

        // PUT: api/ComputersOnService/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputerOnService(int id, ComputerOnService computerOnService)
        {
            if (id != computerOnService.ComputerOnServiceId)
            {
                return BadRequest();
            }

            _context.Entry(computerOnService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerOnServiceExists(id))
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

        // POST: api/ComputersOnService
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComputerOnService>> PostComputerOnService(ComputerOnService computerOnService)
        {
            _context.ComputersOnService.Add(computerOnService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComputerOnService", new { id = computerOnService.ComputerOnServiceId }, computerOnService);
        }

        // DELETE: api/ComputersOnService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerOnService(int id)
        {
            var computerOnService = await _context.ComputersOnService.FindAsync(id);
            if (computerOnService == null)
            {
                return NotFound();
            }

            _context.ComputersOnService.Remove(computerOnService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComputerOnServiceExists(int id)
        {
            return _context.ComputersOnService.Any(e => e.ComputerOnServiceId == id);
        }
    }
}
