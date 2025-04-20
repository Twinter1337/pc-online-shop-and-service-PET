using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICrudService<Order> _orderCrudService;
        private readonly IMapper _mapper;

        public OrderController(ICrudService<Order> orderCrudService, IMapper mapper)
        {
            _orderCrudService = orderCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            List<Order> orders = await _orderCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetFilteredOrders([FromQuery] OrderFilter filter)
        {
            OrdersCrudService ordersCrudService = (_orderCrudService as OrdersCrudService)!;
            List<Order> orders = await ordersCrudService.GetFilteredOrdersAsync(filter);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderCrudService.GetEntityByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDto>(order));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto orderDto)
        {
            if (id != orderDto.OrderId)
            {
                return BadRequest();
            }

            bool result = await _orderCrudService.UpdateEntityAsync(id, _mapper.Map<Order>(orderDto));

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            var createdOrder = _mapper.Map<Order>(orderDto);
            bool result = await _orderCrudService.CreateEntityAsync(createdOrder);

            if (!result)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetOrder), new {id = createdOrder.OrderId},
                _mapper.Map<OrderDto>(createdOrder));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool result = await _orderCrudService.DeleteEntityAsync(id);
            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}