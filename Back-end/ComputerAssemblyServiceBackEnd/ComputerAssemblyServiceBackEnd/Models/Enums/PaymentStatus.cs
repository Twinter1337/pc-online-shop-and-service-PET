using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum PaymentStatus
{
    [PgName("Pending")]
    Pending,
    [PgName("Processing")]
    Processing,
    [PgName("Complited")]
    Complited,
    [PgName("Failed")]
    Failed,
    [PgName("Canceled")]
    Canceled
}