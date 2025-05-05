using System.Xml.Serialization;

namespace ComputerAssemblyServiceBackEnd.Models;

public partial class EmployeePosition
{
    public int PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public int MaximumNumberOfEmpolyees { get; set; }

    [XmlIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
