using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum ServiceStatus
{
    [PgName("New")]
    New, 
    [PgName("InProgress")]
    InProgress, 
    [PgName("Done")]
    Done, 
    [PgName("Closed")]
    Closed, 
    [PgName("Cancelled")]
    Cancelled, 
    [PgName("Faild")]
    Faild
}