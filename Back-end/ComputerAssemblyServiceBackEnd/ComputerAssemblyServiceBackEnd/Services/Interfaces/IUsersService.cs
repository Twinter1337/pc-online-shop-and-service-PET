using ComputerAssemblyServiceBackEnd.Models;

namespace ComputerAssemblyServiceBackEnd.Services.Interfaces;

public interface IUsersService: IService
{
    Task<bool> CreateUser(User user);
    
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByPhoneNumberAsync(string phoneNumber);
    
    Task<bool> UpdateUser(int id, User user);
    Task<bool> DeleteUser(int id);
}
