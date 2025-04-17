using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ComputerAssemblyServiceBackEnd.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Component?> Components { get; set; }
    public virtual DbSet<ComputerOnService> ComputersOnService { get; set; }
    public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<OrderService> OrderServices { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<PatternComponent> PatternComponents { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<PrebuildPattern> PrebuildPatterns { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(
                "Host=localhost;Port=5432;Database=db_course_work;Username=twinter;Password=koksaer123");
            
            dataSourceBuilder.EnableUnmappedTypes();
            NpgsqlConnection.GlobalTypeMapper.EnableDynamicJson();

            var dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource,
                o =>
                {
                    o.MapEnum<ComponentType>("component_type");
                    o.MapEnum<OrderStatus>("order_status");
                    o.MapEnum<PaymentMethod>("payment_method");
                    o.MapEnum<PaymentStatus>("payment_status");
                    o.MapEnum<ProductType>("product_type");
                    o.MapEnum<ServiceStatus>("service_status");
                    o.MapEnum<UserRole>("user_role");
                });
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Component>().ToTable("components");
        modelBuilder.Entity<ComputerOnService>().ToTable("computers_on_service");
        modelBuilder.Entity<Employee>().ToTable("employees");
        modelBuilder.Entity<EmployeePosition>().ToTable("employee_positions");
        modelBuilder.Entity<Order>().ToTable("orders");
        modelBuilder.Entity<OrderItem>().ToTable("order_items");
        modelBuilder.Entity<OrderService>().ToTable("order_services");
        modelBuilder.Entity<PatternComponent>().ToTable("pattern_components");
        modelBuilder.Entity<Payment>().ToTable("payments");
        modelBuilder.Entity<PrebuildPattern>().ToTable("prebuild_patterns");
        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Service>().ToTable("services");
        modelBuilder.Entity<User>().ToTable("users");

        modelBuilder.HasPostgresEnum("component_type",
                new[] { "GPU", "CPU", "RAM", "Motherboard", "HDD", "SSD", "PSU", "Case", "Cooling system" })
            .HasPostgresEnum("order_status",
                new[] { "New", "Confirmed", "Processing", "Paid", "Pending", "Returned", "Complete" })
            .HasPostgresEnum("payment_method", new[] { "Card", "Cash", "Crypto" })
            .HasPostgresEnum("payment_status", new[] { "Pending", "Processing", "Complited", "Failed", "Canceled" })
            .HasPostgresEnum("product_type", new[] { "Computer", "Component" })
            .HasPostgresEnum("service_status", new[] { "New", "In progress", "Done", "Closed", "Cancelled", "Faild" })
            .HasPostgresEnum("user_role", new[] { "Client", "Manager", "Service Worker" });
        
        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.ComponentId).HasName("components_pkey");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.Manufacturer).HasMaxLength(50).HasColumnName("manufacturer");
            entity.Property(e => e.Model).HasMaxLength(50).HasColumnName("model");
            entity.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
            entity.Property(e => e.QuantityOnStock).HasColumnName("quantity_on_stock");
            entity.Property(e => e.Category)
                .HasColumnType("component_type")
                .HasColumnName("category");
            entity.Property(e => e.Characteristics).HasColumnType("jsonb").HasColumnName("characteristics");
        });

        modelBuilder.Entity<ComputerOnService>(entity =>
        {
            entity.HasKey(e => e.ComputerOnServiceId).HasName("computersonservice_pkey");
            entity.Property(e => e.ComputerOnServiceId).HasColumnName("computers_on_service_id")
                .HasDefaultValueSql("nextval('computersonservice_computer_on_service_id_seq'::regclass)");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ResponsibleEmployeeId).HasColumnName("responsible_employee_id");
            entity.Property(e => e.Status).HasColumnType("service_status")
                .HasColumnName("status");
        });

        modelBuilder.Entity<EmployeePosition>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("employeepositions_pkey");
            entity.HasIndex(e => e.PositionName, "employeepositions_position_name_key").IsUnique();
            entity.Property(e => e.PositionId).HasColumnName("position_id")
                .HasDefaultValueSql("nextval('employeepositions_position_id_seq'::regclass)");
            entity.Property(e => e.MaximumNumberOfEmpolyees).HasColumnName("maximum_number_of_empolyees")
                .HasDefaultValue(1);
            entity.Property(e => e.PositionName).HasColumnName("position_name").HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");
            entity.HasIndex(e => e.UserId, "unique_user_id").IsUnique();
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.BankAccount).HasMaxLength(29).IsFixedLength().HasColumnName("bank_account");
            entity.Property(e => e.HireDate).HasDefaultValueSql("CURRENT_DATE").HasColumnName("hire_date");
            entity.Property(e => e.Salary).HasPrecision(10, 2).HasDefaultValueSql("8000").HasColumnName("salary");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("order_item_pkey");
            entity.Property(e => e.ItemId).HasColumnName("item_id")
                .HasDefaultValueSql("nextval('order_item_item_id_seq'::regclass)");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
            entity.Property(e => e.Quantity).HasDefaultValue(1).HasColumnName("quantity");
        });

        modelBuilder.Entity<OrderService>(entity =>
        {
            entity.HasKey(e => e.OrderServiceId).HasName("order_services_pkey");
            entity.Property(e => e.OrderServiceId).HasColumnName("order_service_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ResponsibleEmployeeId).HasColumnName("responsible_employee_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_DATE").HasColumnName("created_at");
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2).HasColumnName("total_amount");
            entity.Property(o => o.Status).HasColumnType("order_status")
                .HasColumnName("status");
        });

        modelBuilder.Entity<PatternComponent>(entity =>
        {
            entity.HasKey(e => e.PatternComponentId).HasName("pattern_components_pkey");
            entity.Property(e => e.PatternComponentId).HasColumnName("pattern_component_id")
                .HasDefaultValueSql("nextval('pattern_components_pattern_components_id_seq'::regclass)");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.PatternId).HasColumnName("pattern_id");
            entity.Property(e => e.Quantity).HasDefaultValue(1).HasColumnName("quantity");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Amount).HasPrecision(10, 2).HasColumnName("amount");
            entity.Property(e => e.Status).HasColumnType("payment_status")
                .HasColumnName("status");
            entity.Property(e => e.Method).HasColumnType("payment_method")
                .HasColumnName("method");
        });

        modelBuilder.Entity<PrebuildPattern>(entity =>
        {
            entity.HasKey(e => e.SerialNumber).HasName("prebuildpatterns_pkey");
            entity.Property(e => e.SerialNumber).HasColumnName("serial_number")
                .HasDefaultValueSql("nextval('prebuildpatterns_serial_number_seq'::regclass)");
            entity.Property(e => e.BasePrice).HasPrecision(10, 2).HasColumnName("base_price");
            entity.Property(e => e.Manufacturer).HasMaxLength(50).HasColumnName("manufacturer");
            entity.Property(e => e.PrebuildName).HasMaxLength(50).HasColumnName("prebuild_name");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Sku).HasName("products_pkey");
            entity.Property(e => e.Sku).HasColumnName("sku");
            entity.Property(e => e.ComponentId).HasColumnName("component_id");
            entity.Property(e => e.ComputerId).HasColumnName("computer_id");
            entity.Property(e => e.Category).HasColumnType("product_type")
                .HasColumnName("category");
            entity.Property(e => e.ImgUrl).HasMaxLength(2048).HasColumnName("img_url");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("services_pkey");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("name");
            entity.Property(e => e.Price).HasPrecision(10, 2).HasColumnName("price");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email).HasMaxLength(254).HasColumnName("email");
            entity.Property(e => e.FirstName).HasMaxLength(50).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasMaxLength(50).HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15).HasColumnName("phone_number");
            entity.Property(e => e.Role).HasColumnType("user_role").HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}