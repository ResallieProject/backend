using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Respositories.Advertisements;
using Resallie.Services.Advertisements;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var serverVersion = new MySqlServerVersion(new Version(10, 9));
        var config = builder.Configuration;

        builder.Services.AddDbContextPool<AppDbContext>(dbContextOptions => dbContextOptions
            .UseMySql(config.GetConnectionString("DefaultConnection"), serverVersion)
            // The following three options help with debugging, but should
            // be changed or removed for production.
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging());

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<AdvertisementRepository>();
        builder.Services.AddScoped<AdvertisementService>();

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