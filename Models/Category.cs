using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bogus;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Models
{
    public class Category : Model
    {
        [StringLength(128)]
        [Required] public string Name { get; set; }
        [StringLength(256)]
        [Required] public string Description { get; set; }

        public override void Seed(AppDbContext appDbContext, int quantity)
        {
            if (!appDbContext.Categories.Any() || quantity > 0)
            {
                var id = 1;
                var a = new Faker<Category>()
                    .RuleFor(m => m.Id, f => id++)
                    .RuleFor(m => m.Name, f => f.Commerce.Product())
                    .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(m => m.CreatedAt, f => DateTime.Now);
            }
        }
    }

}