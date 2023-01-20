using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Resallie.Data;
using Bogus;

namespace Resallie.Models
{
    public class Advertisement : Model
    {
        #region Attributen
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
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        
        public virtual ICollection<AdvertisementFeature>? Features { get; set; }
        #endregion
    }
}