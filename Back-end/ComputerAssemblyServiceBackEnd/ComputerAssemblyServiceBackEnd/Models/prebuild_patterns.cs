using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class prebuild_patterns
{
    public int serial_number { get; set; }

    public string prebuild_name { get; set; } = null!;

    public string manufacturer { get; set; } = null!;

    public string description { get; set; } = null!;

    public decimal base_price { get; set; }

    public virtual ICollection<pattern_components> pattern_components { get; set; } = new List<pattern_components>();

    public virtual products? products { get; set; }
}
