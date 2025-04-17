using ComputerAssemblyServiceBackEnd.Filters.Models;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IPaymentsService: IService
{
    Task<bool> CreatePaymentAsync(Payment payment);
    
    Task<List<Payment>> GetAllPaymentsAsync();
    Task<List<Payment>> GetPaymentsByOrderIdAsync(int orderId);
    Task<List<Payment>> GetFilteredPaymentsAsync(PaymentFilter filter);

    Task<bool> UpdatePaymentAsync(int id, Payment payment);
    Task<bool> DeletePaymentAsync(int id);
}