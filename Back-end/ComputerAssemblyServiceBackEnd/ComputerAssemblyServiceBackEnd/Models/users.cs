using ComputerAssemblyServiceBackEnd.Enums.Models;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class users
{
    public int user_id { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string email { get; set; } = null!;

    public string phone_number { get; set; } = null!;
    
    public User_role role { get; set; }

    public virtual ICollection<computers_on_service> computers_on_service { get; set; } = new List<computers_on_service>();

    public virtual employees? employees { get; set; }
}
