using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.OrderServiceDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;

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
            try
            {
                var orderServices = await _orderServiceCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<OrderServiceDto>>(orderServices));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderServiceDto>> GetOrderService(int id)
        {
            try
            {
                var orderService = await _orderServiceCrudService.GetEntityByIdAsync(id);
                if (orderService == null)
                    return NotFound($"OrderService with ID {id} not found.");

                return Ok(_mapper.Map<OrderServiceDto>(orderService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderService(int id, OrderServiceUpdateDto orderServiceUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (orderServiceUpdateDto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_orderServiceCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync(orderServiceUpdateDto.OrderId);
                    if (order == null)
                        return NotFound($"Order with ID {orderServiceUpdateDto.OrderId} not found.");
                }

                var result = await _orderServiceCrudService.UpdateEntityAsync(id, orderServiceUpdateDto);
                if (!result)
                    return BadRequest($"Failed to update OrderService with ID {id}.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrderService(int id, OrderServicePatchDto orderServicePatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (orderServicePatchDto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_orderServiceCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync((int)orderServicePatchDto.OrderId);
                    if (order == null)
                        return NotFound($"Order with ID {orderServicePatchDto.OrderId} not found.");
                }

                var result = await _orderServiceCrudService.PatchEntityAsync(id, orderServicePatchDto);
                if (!result)
                    return BadRequest($"Failed to patch OrderService with ID {id}.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderServiceDto>> PostOrderService(OrderServiceCreateDto orderServiceCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (orderServiceCreateDto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_orderServiceCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync(orderServiceCreateDto.OrderId);
                    if (order == null)
                        return NotFound($"Order with ID {orderServiceCreateDto.OrderId} not found.");
                }

                var createdOrderService = _mapper.Map<OrderService>(orderServiceCreateDto);
                var result = await _orderServiceCrudService.CreateEntityAsync(createdOrderService);

                if (!result)
                    return BadRequest("Failed to create OrderService.");

                return CreatedAtAction(nameof(GetOrderService), new { id = createdOrderService.OrderServiceId },
                    _mapper.Map<OrderServiceDto>(createdOrderService));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderService(int id)
        {
            try
            {
                var result = await _orderServiceCrudService.DeleteEntityAsync(id);
                if (!result)
                    return NotFound($"OrderService with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}