using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Resallie.Data;
using Resallie.Services.Advertisements;
using Resallie.Services.Categories;
using Resallie.Respositories.Advertisements;
using Resallie.Respositories.Authentication;
using Resallie.Respositories.Categories;
using AuthenticationService = Resallie.Services.Authentication.AuthenticationService;

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
        
        services.AddScoped<AuthenticationRepository>();
        services.AddScoped<AuthenticationService>();

        string[] origins = config["CorsOrigins"].Split(';');
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.WithOrigins(origins)
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        //seeder
        services.AddTransient<DataSeeder>();
        
        // authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            Byte[] signingKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            
            if (signingKey.Length < 32)
            {
                throw new Exception("JWT key must be at least 32 characters long");
            }
            
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (signingKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });

        services.AddAuthorization();

        var app = builder.Build();
        
        app.UseCors("AllowAllOrigins");

        if (args.Length == 2 || args.Length == 3 && args[0].ToLower() == "seeddata")
        {
            string categoryname = args[1];
            int quantity = args.Length > 2 ? int.Parse(args[2]) : 0;
            SeedData(app, categoryname, quantity);
        }            

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.UseAuthentication();

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