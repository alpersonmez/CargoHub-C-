using Cargohub.Models;
using Cargohub.Services;
using Cargohub.DatetimeConverter; // Added this namespace
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
builder.Services.AddScoped<IItemLinesService, ItemLinesService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IOrderShipmentService, OrderShipmentService>();
builder.Services.AddScoped<IDockService, DockService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add controllers
builder.Services.AddControllers();

// Register the DataLoader class as a singleton
builder.Services.AddSingleton<DataLoader>(); // Added this line

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Call the ImportData method after the app is built
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataLoader.ImportData(context); // Call the import function
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add endpoints
app.MapControllers();



// Run the application
app.Run();
