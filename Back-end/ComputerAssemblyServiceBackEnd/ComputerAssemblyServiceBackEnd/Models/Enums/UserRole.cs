using NpgsqlTypes;

namespace ComputerAssemblyServiceBackEnd.Enums.Models;

public enum UserRole
{
    [PgName("Client")]
    Client,
    [PgName("Manager")]
    Manager,
    [PgName("ServiceWorker")]
    ServiceWorker
}