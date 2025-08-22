using ISP_Backend_Dotnet.Application.Services;
using ISP_Backend_Dotnet.Domain.Interfaces;
using ISP_Backend_Dotnet.Persistence.Data;
using ISP_Backend_Dotnet.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ISPContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        builder => builder
            // Update with Flutter app's URL
            .WithOrigins(
                "http://localhost",
                "http://localhost:*",
                "http://10.0.2.2:*",     // For Android emulator
                "http://127.0.0.1:*"     // For iOS simulator
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFlutterApp");
app.UseAuthorization();
app.MapControllers();

app.Run();