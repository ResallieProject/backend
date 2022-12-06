using Microsoft.EntityFrameworkCore;
using Resallie.Models;

namespace Resallie.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementFeature> AdvertisementFeatures { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Advertisement>().Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        
        builder.Entity<Category>().Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
    }
}