using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Services.Advertisements;
using Resallie.Services.Categories;
using Resallie.Respositories.Advertisements;
using Resallie.Respositories.Categories;
using Resallie.Models;

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

        //seeder
        services.AddTransient<DataSeeder>();

        var app = builder.Build();

        if (args.Length == 2 || args.Length == 3 && args[0].ToLower() == "seeddata")
        {
            string categoryname = args[1];
            int quantity = int.Parse(args[2]) != null ? int.Parse(args[2]) : 0;
            SeedData(app, categoryname, quantity);
        }       

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

    private static void SeedData(IHost app, string categoryname, int quantity)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();  
        
        using (var scope = scopedFactory.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<DataSeeder>();
            service.Seed(categoryname, quantity);
        }
    }
}