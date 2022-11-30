using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Resallie.Data;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Resallie.Models
{
    public class Advertisement : Model
    {
        [StringLength(32)]
        [Required] public string Title { get; set; }
        [StringLength(512)]
        [Required] public string Description { get; set; }
        [StringLength(256)]
        [Required] public string Defects { get; set; }
        [Required] public double Price { get; set; }
        [Required] public bool IsExpired { get; set; }
        
        [ForeignKey("Category")]
        [Required] public int CategoryId { get; set; }
        
        public virtual Category? Category { get; set; }
        
        public virtual ICollection<AdvertisementFeature>? Features { get; set; }

        public override void Seed(AppDbContext appDbContext, int quantity)
        {
            var builder = new ModelBuilder();

            if (!appDbContext.Advertisements.Any() || quantity > 0)
            {
                var a = new Faker<Advertisement>()
                    .RuleFor(x => x.Title, x => x.Commerce.ProductName());

                builder.Entity<Advertisement>().HasData(a.GenerateBetween(quantity, quantity));
                appDbContext.SaveChanges();
            }
        }
        
        public override Advertisement FakedModel()
        {
            var id = 2;
            return new Faker<Advertisement>()
                    .RuleFor(m => m.Id, f => id++)
                    .RuleFor(m => m.Title, f => f.Commerce.ProductName())
                    .RuleFor(m => m.Defects, f => f.Music.Genre())
                    .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(m => m.CategoryId, f => 1)
                    .RuleFor(m => m.CreatedAt, f => DateTime.Now);
        }
    }
}