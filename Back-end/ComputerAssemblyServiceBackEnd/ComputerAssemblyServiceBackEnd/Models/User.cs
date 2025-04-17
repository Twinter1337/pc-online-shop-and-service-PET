using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;
    
    public UserRole Role { get; set; }

    public virtual ICollection<ComputerOnService> ComputersOnService { get; set; } = new List<ComputerOnService>();

    public virtual Employee? Employee { get; set; }
}
