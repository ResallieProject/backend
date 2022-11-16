using Microsoft.EntityFrameworkCore;
using Resallie.Models;

namespace Resallie.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<Category> Categories { get; set; }
}