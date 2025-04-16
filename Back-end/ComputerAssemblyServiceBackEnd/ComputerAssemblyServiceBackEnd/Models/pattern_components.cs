using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class pattern_components
{
    public int pattern_component_id { get; set; }

    public int pattern_id { get; set; }

    public int component_id { get; set; }

    public int? quantity { get; set; }

    public virtual components component { get; set; } = null!;

    public virtual prebuild_patterns pattern { get; set; } = null!;
}
