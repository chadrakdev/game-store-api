using GameStoreApi.Interfaces;
using GameStoreApi.Services;

namespace GameStoreApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        
        // Enable generation and support for Swagger documentation and testing
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // Register services for Dependency Injection
        builder.Services.AddScoped<IGameService, GameService>();

        var app = builder.Build();

        // Enable Swagger for development
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection().UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers(); // Maps endpoints defined in controllers

        app.Run();
    }
}