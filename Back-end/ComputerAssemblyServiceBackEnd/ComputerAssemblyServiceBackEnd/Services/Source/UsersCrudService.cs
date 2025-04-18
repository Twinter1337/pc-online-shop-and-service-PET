using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using ComputerAssemblyServiceBackEnd.Services.Interfaces;

namespace ComputerAssemblyServiceBackEnd.Services.Source;

public class UsersCrudService: CrudService<User>
{
    public UsersCrudService(AppDbContext context) : base(context)
    {
        Context = context;
    }

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}