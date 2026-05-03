var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8888");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ILoadCalcService, LoadCalcService>();

var app = builder.Build();

app.MapOpenApi();

app.MapGet("/ping", () => "pong"); //To test the connexion to the API.

app.MapControllers();

app.Run();

