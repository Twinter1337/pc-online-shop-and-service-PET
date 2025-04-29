using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class PaymentsCrudService: CrudService<Payment>
{
    public PaymentsCrudService(AppDbContext context) : base(context)
    {
    }
    
    public async Task<List<Payment>> GetPaymentsByOrderIdAsync(int orderId)
    {
        return await Context.Payments.Where(p => p.OrderId == orderId).ToListAsync();
    }

    public Task<List<Payment>> GetFilteredPaymentsAsync(PaymentFilter filter)
    {
        var query = Context.Payments.AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(p => p.Status == filter.Status);
        }

        if (filter.MaxAmount.HasValue)
        {
            query = query.Where(p => p.Amount <= filter.MaxAmount);
        }

        if (filter.MinAmount.HasValue)
        {
            query = query.Where(p => p.Amount >= filter.MinAmount);
        }

        if (filter.PaymentMethod.HasValue)
        {
            query = query.Where(p => p.Method == filter.PaymentMethod);
        }
        
        return query
            .Include(p => p.Order)
            .ToListAsync();
    }
}