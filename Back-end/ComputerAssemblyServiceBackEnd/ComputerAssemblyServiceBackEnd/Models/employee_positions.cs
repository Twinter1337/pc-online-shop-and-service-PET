using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class employee_positions
{
    public int position_id { get; set; }

    public string position_name { get; set; } = null!;

    public int maximum_number_of_empolyees { get; set; }

    public virtual ICollection<employees> employees { get; set; } = new List<employees>();
}
