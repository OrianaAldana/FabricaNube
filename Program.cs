using FabricaNube.Data;
using FabricaNube.Services;
using Microsoft.EntityFrameworkCore;

var url = Environment.GetEnvironmentVariable("DATABASE_URL");
Console.WriteLine($"La cadena de conexion es esta: {url}");


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FabricaDbContext>(options =>
    options.UseNpgsql(url)
);
//add services
builder.WebHost.UseUrls("http://0.0.0.0:8080");
// Registrar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsLibre", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ISucursalService, SucursalService>();


builder.Services.AddHttpClient("SucursalService", client =>
{
    client.BaseAddress = new Uri("https:sucursalsantacruz-production.up.railway.app/swagger");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FabricaDbContext>();
    db.Database.Migrate();
}

    app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsLibre");

app.UseAuthorization();

app.MapControllers();

app.Run();
