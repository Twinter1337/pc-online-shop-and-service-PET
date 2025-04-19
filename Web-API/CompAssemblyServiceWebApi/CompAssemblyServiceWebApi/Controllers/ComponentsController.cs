using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

namespace ComputerAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly ICrudService<Component> _componentsCrudService;
        private readonly IMapper _mapper;

        public ComponentsController(ICrudService<Component> componentsCrudService, IMapper mapper)
        {
            _componentsCrudService = componentsCrudService;
            _mapper = mapper;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponents()
        {
            List<Component> components = await _componentsCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<ComponentDto>>(components));
        }

        // GET: api/Components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {
            var component = await _componentsCrudService.GetEntityByIdAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            if (id != component.ComponentId)
            {
                return BadRequest();
            }

            await _componentsCrudService.UpdateEntityAsync(id, component);

            return NoContent();
        }

        // // POST: api/Components
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Component>> PostComponent(Component component)
        // {
        //     _context.Components.Add(component);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetComponent", new { id = component.ComponentId }, component);
        // }
        //
        // // DELETE: api/Components/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteComponent(int id)
        // {
        //     var component = await _context.Components.FindAsync(id);
        //     if (component == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Components.Remove(component);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // private bool ComponentExists(int id)
        // {
        //     return _context.Components.Any(e => e.ComponentId == id);
        // }
    }
}
