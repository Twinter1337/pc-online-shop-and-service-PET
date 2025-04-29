using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.OrderDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using QuestPDF.Fluent;

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
            try
            {
                var orders = await _orderCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetFilteredOrders([FromQuery] OrderFilter filter)
        {
            try
            {
                var ordersCrudService = (_orderCrudService as OrdersCrudService)!;
                var orders = await ordersCrudService.GetFilteredOrdersAsync(filter);
                if (orders.Count == 0)
                {
                    return NotFound("No orders found"); 
                }
                return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            try
            {
                var order = await _orderCrudService.GetEntityByIdAsync(id);

                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                return Ok(_mapper.Map<OrderDto>(order));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("OrdersReport/pdf")]
        public async Task<IActionResult> GetPdfReport([FromQuery] DateOnly from, [FromQuery] DateOnly to)
        {
            var ocs = _orderCrudService as OrdersCrudService;
            OrderFilter dateFilter = new OrderFilter()
            {
                MinCreationDate = from,
                MaxCreationDate = to,
            };
            var orders = await ocs.GetFilteredOrdersAsync(dateFilter);
            
            var filteredByStatusOrders = orders.Where(o => o.Status != OrderStatus.New).ToList(); 

            var document = new OrdersReportDocument(filteredByStatusOrders, from, to);
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var pdfBytes = document.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"orders_report_{from:yyyyMMdd}_{to:yyyyMMdd}.pdf");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderUpdateDto orderUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                if (orderUpdateDto.ClientId != null)
                {
                    var ucs = new UsersCrudService(_orderCrudService.Context);
                    var client = await ucs.GetEntityByIdAsync((int)orderUpdateDto.ClientId);

                    if (client == null)
                    {
                        return NotFound($"Client with ID {orderUpdateDto.ClientId} not found.");
                    }
                }

                bool result = await _orderCrudService.UpdateEntityAsync(id, orderUpdateDto);

                if (!result)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderPatchDto orderPatchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                if (orderPatchDto.ClientId != null)
                {
                    var ucs = new UsersCrudService(_orderCrudService.Context);
                    var client = await ucs.GetEntityByIdAsync((int)orderPatchDto.ClientId);

                    if (client == null)
                    {
                        return NotFound($"Client with ID {orderPatchDto.ClientId} not found.");
                    }
                }

                bool result = await _orderCrudService.PatchEntityAsync(id, orderPatchDto);

                if (!result)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderCreateDto orderCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            try
            {
                if (orderCreateDto.ClientId != null)
                {
                    var ucs = new UsersCrudService(_orderCrudService.Context);
                    var client = await ucs.GetEntityByIdAsync((int)orderCreateDto.ClientId);

                    if (client == null)
                    {
                        return NotFound($"Client with ID {orderCreateDto.ClientId} not found.");
                    }
                }

                var createdOrder = _mapper.Map<Order>(orderCreateDto);
                bool result = await _orderCrudService.CreateEntityAsync(createdOrder);

                if (!result)
                {
                    return BadRequest("Failed to create the order.");
                }

                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderId },
                    _mapper.Map<OrderDto>(createdOrder));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                bool result = await _orderCrudService.DeleteEntityAsync(id);

                if (!result)
                {
                    return NotFound($"Order with ID {id} not found.");
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