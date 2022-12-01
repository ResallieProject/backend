using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Services.Advertisements;
using Resallie.Services.Categories;
using Resallie.Respositories.Advertisements;
using Resallie.Respositories.Categories;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var serverVersion = new MySqlServerVersion(new Version(10, 9));
        var config = builder.Configuration;
        var services = builder.Services;

        services.AddDbContextPool<AppDbContext>(dbContextOptions => dbContextOptions
            .UseMySql(config.GetConnectionString("DefaultConnection"), serverVersion)
            // The following three options help with debugging, but should
            // be changed or removed for production.
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging());

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<CategoryRepository>();
        services.AddScoped<CategoryService>();

        services.AddScoped<AdvertisementService>();
        services.AddScoped<AdvertisementRepository>();
        services.AddScoped<AdvertisementFeatureRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}