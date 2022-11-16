using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Resallie.Models
{
    public class Advertisement : Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdvertisementId
        {
            get => Id;
            set => Id = value;
        }

        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Defects { get; set; }
        [Required] public double Price { get; set; }
        [Required] public bool IsExpired { get; set; }
    }
}