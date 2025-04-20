using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderServiceController : ControllerBase
    {
        private readonly ICrudService<OrderService> _orderServiceCrudService;
        private readonly IMapper _mapper;

        public OrderServiceController(ICrudService<OrderService> orderServiceCrudService, IMapper mapper)
        {
            _orderServiceCrudService = orderServiceCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderServiceDto>>> GetOrderServices()
        {
            List<OrderService> orderServices = await _orderServiceCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<OrderServiceDto>>(orderServices));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderServiceDto>> GetOrderService(int id)
        {
            var orderService = await _orderServiceCrudService.GetEntityByIdAsync(id);

            if (orderService == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderServiceDto>(orderService));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderService(int id, OrderServiceDto orderServiceDto)
        {
            if (id != orderServiceDto.OrderServiceId)
            {
                return BadRequest();
            }

            bool result =
                await _orderServiceCrudService.UpdateEntityAsync(id, _mapper.Map<OrderService>(orderServiceDto));

            if (!result)
            {
                return BadRequest();
            }
            
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<OrderServiceDto>> PostOrderService(OrderServiceDto orderServiceDto)
        {
            var createdOrderService = _mapper.Map<OrderService>(orderServiceDto);
            bool result = await _orderServiceCrudService.CreateEntityAsync(createdOrderService);

            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetOrderService), new { id = createdOrderService.OrderServiceId },
                _mapper.Map<OrderItemDto>(createdOrderService));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderService(int id)
        {
            bool result = await _orderServiceCrudService.DeleteEntityAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}