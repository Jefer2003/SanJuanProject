
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using static SanJuanProject.Data.AppData;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
var AllowAll = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAll, policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Configuración de la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
 ?? Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlServer(connectionString)
);

// Configuración de controladores y JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar puerto dinámico para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Swagger siempre habilitado
app.UseSwagger();
app.UseSwaggerUI();

// No usar HTTPS redirection en Render
// app.UseHttpsRedirection();

app.UseCors(AllowAll);

app.UseAuthorization();

app.MapControllers();

app.Run();
