using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Resallie.Models
{
    public class AdvertisementFeature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        
        [StringLength(256)]
        [Required] public string Key { get; set; }
        [StringLength(256)]
        [Required] public string Value { get; set; }
        
        [JsonIgnore]
        [ForeignKey("Advertisement")]
        [Required] public int AdvertisementId { get; set; }
    }
}