using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class PaymentsService: IPaymentsService
{
    private AppDbContext _context;
    public AppDbContext Context => _context;

    public PaymentsService(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> CreatePaymentAsync(Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task<List<Payment>> GetAllPaymentsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Payment>> GetPaymentsByOrderIdAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Payment>> GetFilteredPaymentsAsync(PaymentFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePaymentAsync(int id, Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeletePaymentAsync(int id)
    {
        throw new NotImplementedException();
    }
}