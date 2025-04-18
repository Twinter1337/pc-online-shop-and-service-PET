using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class OrderServicesCrudService: CrudService<OrderService>
{
    public OrderServicesCrudService(AppDbContext context) : base(context)
    {
    }
}