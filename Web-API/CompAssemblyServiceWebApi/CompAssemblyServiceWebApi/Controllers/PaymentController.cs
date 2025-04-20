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
            List<Payment> payments = await _paymentCrudService.GetAllEntitiesAsync();
            return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
        }
        
        [HttpGet("by-order/{orderId}")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPaymentsByOrderId(int orderId)
        {
            PaymentsCrudService paymentsCrudService = (_paymentCrudService as PaymentsCrudService)!;
            List<Payment> payments = await paymentsCrudService.GetPaymentsByOrderIdAsync(orderId);
            return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetFilteredPayments([FromQuery] PaymentFilter filter)
        {
            PaymentsCrudService paymentsCrudService = (_paymentCrudService as PaymentsCrudService)!;
            List<Payment> payments = await paymentsCrudService.GetFilteredPaymentsAsync(filter);
            return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentCrudService.GetEntityByIdAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PaymentDto>(payment));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDto paymentDto)
        {
            if (id != paymentDto.PaymentId)
            {
                return BadRequest();
            }

            bool result = await _paymentCrudService.UpdateEntityAsync(id, _mapper.Map<Payment>(paymentDto));

            if (!result)
            {
                return BadRequest();
            }
            
            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> PostPayment(PaymentDto paymentDto)
        {
            var createdPayment = _mapper.Map<Payment>(paymentDto);
            bool result = await _paymentCrudService.CreateEntityAsync(createdPayment);

            if (!result)
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetPayment), new { id = createdPayment.PaymentId },
                _mapper.Map<PaymentDto>(createdPayment));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            bool result = await _paymentCrudService.DeleteEntityAsync(id);

            if (!result)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}