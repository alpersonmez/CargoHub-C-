var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.Urls.Add("http://localhost:5000");

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
