using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum OrderStatus
{
    [PgName("New")]
    New,
    [PgName("Confirmed")]
    Confirmed,
    [PgName("Pending")]
    Processing,
    [PgName("Paid")]
    Paid,
    [PgName("Pending")]
    Pending,
    [PgName("Returned")]
    Returned,
    [PgName("Complete")]
    Complete
}