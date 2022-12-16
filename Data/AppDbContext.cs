using Microsoft.EntityFrameworkCore;
using Resallie.Models;
using Resallie.Models.Advertisements;

namespace Resallie.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementFeature> AdvertisementFeatures { get; set; }
    public DbSet<AdvertisementImage> AdvertisementImage { get; set; }
   
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Advertisement>().Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        
        builder.Entity<Category>().Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        builder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.CategoryId);
        
        builder.Entity<User>().Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
    }
}