using System;
using System.Collections.Generic;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class PatternComponent
{
    public int PatternComponentId { get; set; }

    public int PatternId { get; set; }

    public int ComponentId { get; set; }

    public int? Quantity { get; set; }

    public virtual Component Component { get; set; } = null!;

    public virtual PrebuildPattern Pattern { get; set; } = null!;
}
