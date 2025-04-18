using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class OrderServicesCrudService: CrudService<OrderService>
{
    public OrderServicesCrudService(AppDbContext context) : base(context)
    {
    }
}