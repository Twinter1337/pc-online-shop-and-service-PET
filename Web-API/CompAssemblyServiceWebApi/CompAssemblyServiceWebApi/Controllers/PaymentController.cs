using AutoMapper;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PatchDtos;
using ComputerAssemblyServiceBackEnd.Models.Dtos.PaymentDtos;

namespace CompAssemblyServiceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ICrudService<Payment> _paymentCrudService;
        private readonly IMapper _mapper;

        public PaymentController(ICrudService<Payment> paymentCrudService, IMapper mapper)
        {
            _paymentCrudService = paymentCrudService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            try
            {
                var payments = await _paymentCrudService.GetAllEntitiesAsync();
                return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("by-order/{orderId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByOrderId(int orderId)
        {
            try
            {
                if (_paymentCrudService is not PaymentsCrudService paymentsService)
                    return StatusCode(500, "Internal service error: cannot cast to PaymentsCrudService");

                var payments = await paymentsService.GetPaymentsByOrderIdAsync(orderId);
                return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetFilteredPayments([FromQuery] PaymentFilter filter)
        {
            try
            {
                if (_paymentCrudService is not PaymentsCrudService paymentsService)
                    return StatusCode(500, "Internal service error: cannot cast to PaymentsCrudService");

                var payments = await paymentsService.GetFilteredPaymentsAsync(filter);
                return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            try
            {
                var payment = await _paymentCrudService.GetEntityByIdAsync(id);
                if (payment == null)
                    return NotFound($"Payment with ID {id} not found");

                return Ok(_mapper.Map<PaymentDto>(payment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_paymentCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync(dto.OrderId);
                    if (order == null)
                        return NotFound($"Order with ID {dto.OrderId} not found");
                }

                var result = await _paymentCrudService.UpdateEntityAsync(id, dto);
                if (!result)
                    return BadRequest($"Failed to update Payment with ID {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchPayment(int id, PaymentPatchDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_paymentCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync((int)dto.OrderId);
                    if (order == null)
                        return NotFound($"Order with ID {dto.OrderId} not found");
                }

                var result = await _paymentCrudService.PatchEntityAsync(id, dto);
                if (!result)
                    return BadRequest($"Failed to patch Payment with ID {id}");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> PostPayment(PaymentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (dto.OrderId != null)
                {
                    var ocs = new OrdersCrudService(_paymentCrudService.Context);
                    var order = await ocs.GetEntityByIdAsync(dto.OrderId);
                    if (order == null)
                        return NotFound($"Order with ID {dto.OrderId} not found");
                }

                var created = _mapper.Map<Payment>(dto);
                var result = await _paymentCrudService.CreateEntityAsync(created);
                if (!result)
                    return BadRequest("Failed to create Payment");

                return CreatedAtAction(nameof(GetPayment), new { id = created.PaymentId },
                    _mapper.Map<PaymentDto>(created));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var result = await _paymentCrudService.DeleteEntityAsync(id);
                if (!result)
                    return NotFound($"Payment with ID {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}