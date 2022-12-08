using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resallie.Data;
using Bogus;


namespace Resallie.Models
{
    public class Category : Model
    {
        #region Attributen
        [StringLength(128)]
        [Required] public string Name { get; set; }
        [StringLength(256)]
        [Required] public string Description { get; set; }
        
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        #endregion

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