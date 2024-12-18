using Cargohub.Data;
using Cargohub.Services;
using Cargohub.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Register the database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=CargoHub.db"));

// Register services
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IItemTypeService, ItemTypeService>();
builder.Services.AddScoped<IItemGroupService, ItemGroupService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IitemlinesService, ItemlinesService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers
builder.Services.AddControllers();
var app = builder.Build();

app.Urls.Add("http://localhost:5000");
app.MapControllers();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Add middleware
// app.Use(async (context, next) =>
// {
//     if (context.Request.Path == "/api")
//     {
//         if (!context.Request.Headers.ContainsKey("API_key"))
//         {
//             Console.WriteLine($"{context.Request.Path} was requested but there is no API_key header");
//             context.Response.StatusCode = 401;
//             return;
//         }
//     }
//     await next.Invoke();
//     Console.WriteLine($"{context.Request.Path} was handled");
// });

// Seed the database with initial data (Optional)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    ItemSeeder.SeedDatabase(context);
}

// Run the application
app.Run();
