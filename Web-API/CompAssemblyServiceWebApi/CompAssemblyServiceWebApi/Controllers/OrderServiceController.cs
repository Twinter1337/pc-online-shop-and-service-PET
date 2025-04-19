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
    public class OrderServiceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderServiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderService>>> GetOrderServices()
        {
            return await _context.OrderServices.ToListAsync();
        }

        // GET: api/OrderService/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderService>> GetOrderService(int id)
        {
            var orderService = await _context.OrderServices.FindAsync(id);

            if (orderService == null)
            {
                return NotFound();
            }

            return orderService;
        }

        // PUT: api/OrderService/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderService(int id, OrderService orderService)
        {
            if (id != orderService.OrderServiceId)
            {
                return BadRequest();
            }

            _context.Entry(orderService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderServiceExists(id))
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

        // POST: api/OrderService
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderService>> PostOrderService(OrderService orderService)
        {
            _context.OrderServices.Add(orderService);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderService", new { id = orderService.OrderServiceId }, orderService);
        }

        // DELETE: api/OrderService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderService(int id)
        {
            var orderService = await _context.OrderServices.FindAsync(id);
            if (orderService == null)
            {
                return NotFound();
            }

            _context.OrderServices.Remove(orderService);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderServiceExists(int id)
        {
            return _context.OrderServices.Any(e => e.OrderServiceId == id);
        }
    }
}
