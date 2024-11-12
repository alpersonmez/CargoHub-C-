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
builder.Services.AddScoped<ILocationService, LocationService>();
   
// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

app.Urls.Add("http://localhost:5000");
app.MapControllers();
app.Run();
