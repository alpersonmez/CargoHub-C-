using Cargohub.Data;
using Cargohub.Services;
using Cargohub.Models;
using Cargohub.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register the database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=CargoHub.db"));

// Register services
builder.Services.AddScoped<IItemService, ItemService>();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddScoped<IOrderService, OrderService>();
var app = builder.Build();

app.Urls.Add("http://localhost:5000");
app.MapControllers();

// Add middleware
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/api/Orders")
    {
        if (!context.Request.Headers.ContainsKey("API_key"))
        {
            Console.WriteLine($"{context.Request.Path} was requested but there is no HelloApiToken header");
            context.Response.StatusCode = 401;
            return;
        }
    }
    await next.Invoke();
    Console.WriteLine($"{context.Request.Path} was handled");
});

app.Run();
