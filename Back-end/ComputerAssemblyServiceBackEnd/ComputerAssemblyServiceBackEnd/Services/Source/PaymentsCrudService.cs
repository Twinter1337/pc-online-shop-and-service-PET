using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class PaymentsCrudService: CrudService<Payment>
{
    public PaymentsCrudService(AppDbContext context) : base(context)
    {
    }
    
    public Task<List<Payment>> GetPaymentsByOrderIdAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Payment>> GetFilteredPaymentsAsync(PaymentFilter filter)
    {
        throw new NotImplementedException();
    }
}