using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerAssemblyServiceBackEnd.CrudServices.Source;

public class UsersCrudService : CrudService<User>
{
    public UsersCrudService(AppDbContext context) : base(context)
    {
        Context = context;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await Context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email)) ??
               throw new InvalidOperationException();
    }

    public async Task<User> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        return await Context.Users.FirstOrDefaultAsync(user => user.PhoneNumber.Equals(phoneNumber)) ??
               throw new InvalidOperationException();
    }
}