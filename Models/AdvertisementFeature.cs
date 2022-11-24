using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Resallie.Models
{
    public class AdvertisementFeature
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(256)]
        [Required] public string Key { get; set; }
        [StringLength(256)]
        [Required] public string Value { get; set; }
        
        [ForeignKey("Advertisement")]
        [Required] public int AdvertisementId { get; set; }
        
        public virtual Advertisement? Advertisement { get; set; }
    }
}