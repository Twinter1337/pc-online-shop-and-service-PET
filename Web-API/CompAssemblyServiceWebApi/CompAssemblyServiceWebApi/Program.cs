using ComputerAssemblyServiceBackEnd.Data;
using ComputerAssemblyServiceBackEnd.CrudServices.Interfaces;
using ComputerAssemblyServiceBackEnd.CrudServices.Source;
using ComputerAssemblyServiceBackEnd.OtpService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using ComputerAssemblyServiceBackEnd.Enums.Models;
using ComputerAssemblyServiceBackEnd.Mappings;
using ComputerAssemblyServiceBackEnd.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// ===== Add services to container =====
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// ===== Database Configuration =====
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
dataSourceBuilder.MapEnum<ComponentType>("component_type");
dataSourceBuilder.MapEnum<OrderStatus>("order_status");
dataSourceBuilder.MapEnum<PaymentStatus>("payment_status");
dataSourceBuilder.MapEnum<PaymentMethod>("payment_method");
dataSourceBuilder.MapEnum<UserRole>("user_role");
dataSourceBuilder.MapEnum<ServiceStatus>("service_status");
dataSourceBuilder.MapEnum<ProductType>("product_type");
dataSourceBuilder.EnableUnmappedTypes();
dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies()
        .UseNpgsql(dataSource, o =>
        {
            o.MapEnum<ComponentType>("component_type");
            o.MapEnum<OrderStatus>("order_status");
            o.MapEnum<PaymentMethod>("payment_method");
            o.MapEnum<PaymentStatus>("payment_status");
            o.MapEnum<ProductType>("product_type");
            o.MapEnum<ServiceStatus>("service_status");
            o.MapEnum<UserRole>("user_role");
        }));

// ===== JWT Configuration =====
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is not configured");
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ===== Dependency Injection =====
builder.Services.AddScoped<OtpService>();
builder.Services.AddScoped<ICrudService<Component>, ComponentsCrudService>();
builder.Services.AddScoped<ICrudService<ComputerOnService>, ComputersOnCrudServiceCrudService>();
builder.Services.AddScoped<ICrudService<Payment>, PaymentsCrudService>();
builder.Services.AddScoped<ICrudService<PrebuildPattern>, PrebuildPatternsCrudService>();
builder.Services.AddScoped<ICrudService<PatternComponent>, PatternComponentsCrudService>();
builder.Services.AddScoped<ICrudService<User>, UsersCrudService>();
builder.Services.AddScoped<ICrudService<Service>, ServicesCrudService>();
builder.Services.AddScoped<ICrudService<Order>, OrdersCrudService>();
builder.Services.AddScoped<ICrudService<OrderItem>, OrderItemsCrudService>();
builder.Services.AddScoped<ICrudService<OrderService>, OrderServicesCrudService>();
builder.Services.AddScoped<ICrudService<Employee>, EmployeesCrudService>();
builder.Services.AddScoped<ICrudService<EmployeePosition>, EmployeePositionsCrudService>();
builder.Services.AddScoped<ICrudService<Product>, ProductsCrudService>();

// ===== Swagger Configuration =====
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BeePC API",
        Version = "v1",
        Description = "API for Computer Assembly Service",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "contact@example.com"
        }
    });

    // Add JWT Authentication support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// ===== Middleware Pipeline =====
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeePC API v1");
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ===== Database Migration =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();