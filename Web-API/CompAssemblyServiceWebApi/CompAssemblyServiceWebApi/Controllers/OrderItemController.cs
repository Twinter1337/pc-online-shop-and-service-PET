using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly ICrudService<OrderItem> _orderItemCrudService;
        private readonly IMapper _mapper;

        public OrderItemController(ICrudService<OrderItem> orderItemCrudService, IMapper mapper)
        {
            _orderItemCrudService = orderItemCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItems()
        {
            List<OrderItem> orderItems = await _orderItemCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(orderItems));
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetFilterOrderItems([FromQuery]OrderItemFilter filter)
        {
            OrderItemsCrudService orderItemsCrudService = (_orderItemCrudService as OrderItemsCrudService)!;
            List<OrderItem> orderItems = await orderItemsCrudService.GetFilteredOrderItemsAsync(filter);
            return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(orderItems));
        }
        
        [HttpGet("by-order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItemByOrderId(int orderId)
        {
            OrderItemsCrudService orderItemsCrudService = (_orderItemCrudService as OrderItemsCrudService)!;
            List<OrderItem> orderItems = await orderItemsCrudService.GetOrderItemsByOrderIdAsync(orderId);

            return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(orderItems));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
        {
            var orderItem = await _orderItemCrudService.GetEntityByIdAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderItemDto>(orderItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItemDto orderItemDto)
        {
            if (id != orderItemDto.ItemId)
            {
                return BadRequest();
            }

            bool result = await _orderItemCrudService.UpdateEntityAsync(id, _mapper.Map<OrderItem>(orderItemDto));

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> PostOrderItem(OrderItemDto orderItemDto)
        {
            var createdOrderItem = _mapper.Map<OrderItem>(orderItemDto);
            bool result = await _orderItemCrudService.CreateEntityAsync(createdOrderItem);

            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetOrderItem), new { id = createdOrderItem.ItemId },
                _mapper.Map<OrderItemDto>(createdOrderItem));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            bool result = await _orderItemCrudService.DeleteEntityAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}