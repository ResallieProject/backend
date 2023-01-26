using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Resallie.Data;
using Bogus;
using Resallie.Models.Advertisements;

namespace Resallie.Models
{
    public class Category : Model
    {
        [StringLength(128)]
        [Required] public string Name { get; set; }
        [StringLength(256)]
        [Required] public string Description { get; set; }
        
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        // This prevents EF from doing kaboom (otherwise it would load the parent and create an infinite loop that consumes the universe)
        [JsonIgnore] 
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? Children { get; set; }
        public virtual ICollection<Advertisement>? Advertisements { get; set; }

        public override void Seed(AppDbContext appDbContext, int quantity)
        {
            if (!appDbContext.Categories.Any() || quantity > 0)
            {
                quantity= quantity == 0 ? quantity = 10 : quantity;
                for (int i = 0; i < quantity; i++)
                {
                     appDbContext.Categories.Add(new Faker<Category>()
                    .RuleFor(m => m.Name, f => f.Commerce.Product())
                    .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(m => m.CreatedAt, f => DateTime.Now));
                }
                appDbContext.SaveChanges();
            }
        }
    }
}