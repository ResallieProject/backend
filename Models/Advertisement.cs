using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Resallie.Data;
using Bogus;

namespace Resallie.Models
{
    public class Advertisement : Model
    {
        #region Atributen
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
        #endregion

        public override void Seed(AppDbContext appDbContext, int quantity)
        {
            if (!appDbContext.Advertisements.Any() || quantity > 0)
            {
                quantity = quantity == 0 ? quantity = 10 : quantity;
                for (int i = 0; i < quantity; i++)
                {
                     appDbContext.Advertisements.Add( new Faker<Advertisement>()
                    .RuleFor(m => m.Title, f => f.Commerce.ProductName())
                    .RuleFor(m => m.Defects, f => f.Music.Genre())
                    .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(m => m.CategoryId, f => 1)
                    .RuleFor(m => m.CreatedAt, f => DateTime.Now));
                }
                appDbContext.SaveChanges();
            }
        }
    }
}