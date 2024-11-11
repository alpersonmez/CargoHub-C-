using Cargohub.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=CargoHub.db"));

builder.Services.AddControllers();

var app = builder.Build();

app.Urls.Add("http://localhost:5000");

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
