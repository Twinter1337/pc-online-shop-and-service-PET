using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;

namespace ComputerAssemblyServiceBackEnd.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<components> components { get; set; }

    public virtual DbSet<computers_on_service> computers_on_service { get; set; }

    public virtual DbSet<employee_positions> employee_positions { get; set; }

    public virtual DbSet<employees> employees { get; set; }

    public virtual DbSet<order_items> order_items { get; set; }

    public virtual DbSet<order_services> order_services { get; set; }

    public virtual DbSet<orders> orders { get; set; }

    public virtual DbSet<pattern_components> pattern_components { get; set; }

    public virtual DbSet<payments> payments { get; set; }

    public virtual DbSet<prebuild_patterns> prebuild_patterns { get; set; }

    public virtual DbSet<products> products { get; set; }

    public virtual DbSet<services> services { get; set; }

    public virtual DbSet<users> users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Port=5432;Database=db_course_work;Username=twinter;Password=koksaer123");

            dataSourceBuilder.MapEnum<Component_type>("component_type");
            dataSourceBuilder.MapEnum<Order_status>("order_status");
            dataSourceBuilder.MapEnum<Payment_method>("payment_method");
            dataSourceBuilder.MapEnum<Payment_status>("payment_status");
            dataSourceBuilder.MapEnum<Product_type>("product_type");
            dataSourceBuilder.MapEnum<Service_status>("service_status");
            dataSourceBuilder.MapEnum<User_role>("user_role");

            dataSourceBuilder.EnableUnmappedTypes();
            var dataSource = dataSourceBuilder.Build();

            optionsBuilder.UseNpgsql(dataSource);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("component_type", new[] { "GPU", "CPU", "RAM", "Motherboard", "HDD", "SSD", "PSU", "Case", "Cooling system" }) 
            .HasPostgresEnum("order_status", new[] { "New", "Confirmed", "Processing", "Paid", "Pending", "Returned", "Complete" }) 
            .HasPostgresEnum("payment_method", new[] { "Card", "Cash", "Crypto" }) 
            .HasPostgresEnum("payment_status", new[] { "Pending", "Processing", "Complited", "Failed", "Canceled" }) 
            .HasPostgresEnum("product_type", new[] { "Computer", "Component" }) 
            .HasPostgresEnum("service_status", new[] { "New", "In progress", "Done", "Closed", "Cancelled", "Faild" }) 
            .HasPostgresEnum("user_role", new[] { "Client", "Manager", "Service Worker" }); 
        
        modelBuilder.Entity<components>(entity =>
        {
            entity.HasKey(e => e.component_id).HasName("components_pkey");

            entity.Property(e => e.manufacturer).HasMaxLength(50);
            entity.Property(e => e.model).HasMaxLength(50);
            entity.Property(e => e.price).HasPrecision(10, 2);
            
            entity.Property(e=>e.category).HasColumnType("component_type").HasConversion<string>();
        });

        modelBuilder.Entity<computers_on_service>(entity =>
        {
            entity.HasKey(e => e.computer_on_service_id).HasName("computersonservice_pkey");

            entity.Property(e => e.computer_on_service_id).HasDefaultValueSql("nextval('computersonservice_computer_on_service_id_seq'::regclass)");

            entity.HasOne(d => d.responsible_employee).WithMany(p => p.computers_on_service)
                .HasForeignKey(d => d.responsible_employee_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("computersonservice_responsible_employee_id_fkey");

            entity.HasOne(d => d.user).WithMany(p => p.computers_on_service)
                .HasForeignKey(d => d.user_id)
                .HasConstraintName("computersonservice_user_id_fkey");
            
            entity.Property(e=>e.status).HasColumnType("service_status").HasConversion<string>();
        });

        modelBuilder.Entity<employee_positions>(entity =>
        {
            entity.HasKey(e => e.position_id).HasName("employeepositions_pkey");

            entity.HasIndex(e => e.position_name, "employeepositions_position_name_key").IsUnique();

            entity.Property(e => e.position_id).HasDefaultValueSql("nextval('employeepositions_position_id_seq'::regclass)");
            entity.Property(e => e.maximum_number_of_empolyees).HasDefaultValue(1);
            entity.Property(e => e.position_name).HasMaxLength(50);
        });

        modelBuilder.Entity<employees>(entity =>
        {
            entity.HasKey(e => e.employee_id).HasName("employees_pkey");

            entity.HasIndex(e => e.user_id, "unique_user_id").IsUnique();

            entity.Property(e => e.bank_account)
                .HasMaxLength(29)
                .IsFixedLength();
            entity.Property(e => e.hire_date).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.salary)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("8000");

            entity.HasOne(d => d.positionNavigation).WithMany(p => p.employees)
                .HasForeignKey(d => d.position)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_position_fkey");

            entity.HasOne(d => d.user).WithOne(p => p.employees)
                .HasForeignKey<employees>(d => d.user_id)
                .HasConstraintName("employees_user_id_fkey");
        });

        modelBuilder.Entity<order_items>(entity =>
        {
            entity.HasKey(e => e.item_id).HasName("order_item_pkey");

            entity.Property(e => e.item_id).HasDefaultValueSql("nextval('order_item_item_id_seq'::regclass)");
            entity.Property(e => e.price).HasPrecision(10, 2);
            entity.Property(e => e.quantity).HasDefaultValue(1);

            entity.HasOne(d => d.order).WithMany(p => p.order_items)
                .HasForeignKey(d => d.order_id)
                .HasConstraintName("order_item_order_id_fkey");

            entity.HasOne(d => d.product).WithMany(p => p.order_items)
                .HasForeignKey(d => d.product_id)
                .HasConstraintName("order_item_product_id_fkey");
        });

        modelBuilder.Entity<order_services>(entity =>
        {
            entity.HasKey(e => e.order_service_id).HasName("order_services_pkey");

            entity.HasOne(d => d.order).WithMany(p => p.order_services)
                .HasForeignKey(d => d.order_id)
                .HasConstraintName("order_services_order_id_fkey");

            entity.HasOne(d => d.responsible_employee).WithMany(p => p.order_services)
                .HasForeignKey(d => d.responsible_employee_id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("order_services_responsible_employee_id_fkey");

            entity.HasOne(d => d.service).WithMany(p => p.order_services)
                .HasForeignKey(d => d.service_id)
                .HasConstraintName("order_services_service_id_fkey");
        });

        modelBuilder.Entity<orders>(entity =>
        {
            entity.HasKey(e => e.order_id).HasName("orders_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.total_amount).HasPrecision(10, 2);
            entity.Property(o => o.status).HasColumnType("order_status").HasConversion<string>();
        });

        modelBuilder.Entity<pattern_components>(entity =>
        {
            entity.HasKey(e => e.pattern_component_id).HasName("pattern_components_pkey");

            entity.Property(e => e.pattern_component_id).HasDefaultValueSql("nextval('pattern_components_pattern_components_id_seq'::regclass)");
            entity.Property(e => e.quantity).HasDefaultValue(1);

            entity.HasOne(d => d.component).WithMany(p => p.pattern_components)
                .HasForeignKey(d => d.component_id)
                .HasConstraintName("patterncomponents_component_id_fkey");

            entity.HasOne(d => d.pattern).WithMany(p => p.pattern_components)
                .HasForeignKey(d => d.pattern_id)
                .HasConstraintName("patterncomponents_pattern_id_fkey");
        });

        modelBuilder.Entity<payments>(entity =>
        {
            entity.HasKey(e => e.payment_id).HasName("payments_pkey");

            entity.Property(e => e.amount).HasPrecision(10, 2);

            entity.HasOne(d => d.order).WithMany(p => p.payments)
                .HasForeignKey(d => d.order_id)
                .HasConstraintName("payments_order_id_fkey");
            
            entity.Property(e=>e.status).HasColumnType("payment_status").HasConversion<string>();  
            entity.Property(e=>e.method).HasColumnType("payment_method").HasConversion<string>();  
        });

        modelBuilder.Entity<prebuild_patterns>(entity =>
        {
            entity.HasKey(e => e.serial_number).HasName("prebuildpatterns_pkey");

            entity.Property(e => e.serial_number).HasDefaultValueSql("nextval('prebuildpatterns_serial_number_seq'::regclass)");
            entity.Property(e => e.base_price).HasPrecision(10, 2);
            entity.Property(e => e.manufacturer).HasMaxLength(50);
            entity.Property(e => e.prebuild_name).HasMaxLength(50);
            entity.Property(e => e.img_url).HasMaxLength(2048);
        });

        modelBuilder.Entity<products>(entity =>
        {
            entity.HasKey(e => e.sku).HasName("products_pkey");

            entity.HasIndex(e => e.component_id, "products_component_id_key").IsUnique();

            entity.HasIndex(e => e.computer_id, "products_computer_id_key").IsUnique();

            entity.HasOne(d => d.component).WithOne(p => p.products)
                .HasForeignKey<products>(d => d.component_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("products_component_id_fkey");

            entity.HasOne(d => d.computer).WithOne(p => p.products)
                .HasForeignKey<products>(d => d.computer_id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("products_computer_id_fkey");
            
            entity.Property(e=>e.category).HasColumnType("product_type").HasConversion<string>();
        });

        modelBuilder.Entity<services>(entity =>
        {
            entity.HasKey(e => e.service_id).HasName("services_pkey");

            entity.Property(e => e.name).HasMaxLength(100);
            entity.Property(e => e.price).HasPrecision(10, 2);
        });

        modelBuilder.Entity<users>(entity =>
        {
            entity.HasKey(e => e.user_id).HasName("users_pkey");

            entity.HasIndex(e => e.email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.phone_number, "users_phone_number_key").IsUnique();

            entity.Property(e => e.email).HasMaxLength(254);
            entity.Property(e => e.first_name).HasMaxLength(50);
            entity.Property(e => e.last_name).HasMaxLength(50);
            entity.Property(e => e.phone_number).HasMaxLength(15);

            entity.Property(e => e.role).HasColumnType("user_role").HasConversion<string>();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
