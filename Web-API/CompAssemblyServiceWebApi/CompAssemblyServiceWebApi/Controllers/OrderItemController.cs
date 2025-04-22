using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.OrderItemDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

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
            try
            {
                List<OrderItem> orderItems = await _orderItemCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(orderItems));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetFilterOrderItems([FromQuery] OrderItemFilter filter)
        {
            try
            {
                var orderItemsCrudService = (_orderItemCrudService as OrderItemsCrudService)!;
                List<OrderItem> orderItems = await orderItemsCrudService.GetFilteredOrderItemsAsync(filter);
                return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(orderItems));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItemByOrderId(int orderId)
        {
            try
            {
                var orderItemsCrudService = (_orderItemCrudService as OrderItemsCrudService)!;
                List<OrderItem> orderItems = await orderItemsCrudService.GetOrderItemsByOrderIdAsync(orderId);
                return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(orderItems));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
        {
            try
            {
                var orderItem = await _orderItemCrudService.GetEntityByIdAsync(id);

                if (orderItem == null)
                {
                    return NotFound($"OrderItem with ID {id} not found.");
                }

                return Ok(_mapper.Map<OrderItemDto>(orderItem));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(int id, OrderItemUpdateDto orderItemUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (orderItemUpdateDto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_orderItemCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync(orderItemUpdateDto.OrderId);

                    if (order == null)
                    {
                        return NotFound($"Order with ID {orderItemUpdateDto.OrderId} not found.");
                    }
                }

                bool result = await _orderItemCrudService.PatchEntityAsync(id, orderItemUpdateDto);

                if (!result)
                {
                    return BadRequest($"Failed to update OrderItem with ID {id}.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrderItem(int id, OrderItemPatchDto orderItemPatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (orderItemPatchDto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_orderItemCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync((int)orderItemPatchDto.OrderId);

                    if (order == null)
                    {
                        return NotFound($"Order with ID {orderItemPatchDto.OrderId} not found.");
                    }
                }

                bool result = await _orderItemCrudService.PatchEntityAsync(id, orderItemPatchDto);

                if (!result)
                {
                    return BadRequest($"Failed to patch OrderItem with ID {id}.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> PostOrderItem(OrderItemCreateDto orderItemCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (orderItemCreateDto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_orderItemCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync(orderItemCreateDto.OrderId);

                    if (order == null)
                    {
                        return NotFound($"Order with ID {orderItemCreateDto.OrderId} not found.");
                    }
                }

                var createdOrderItem = _mapper.Map<OrderItem>(orderItemCreateDto);
                bool result = await _orderItemCrudService.CreateEntityAsync(createdOrderItem);

                if (!result)
                {
                    return BadRequest("Failed to create OrderItem.");
                }

                return CreatedAtAction(nameof(GetOrderItem), new { id = createdOrderItem.ItemId },
                    _mapper.Map<OrderItemDto>(createdOrderItem));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                bool result = await _orderItemCrudService.DeleteEntityAsync(id);
                if (!result)
                {
                    return NotFound($"OrderItem with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}