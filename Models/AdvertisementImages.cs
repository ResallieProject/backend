using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Resallie.Models
{
    public class AdvertisementImages : Model
    {
        [Required]
        public string Path { get; set; }
        public uint Order { get; set; }

        [ForeignKey("Advertisement")]
        [Required]
        public int AdvertisementId { get; set; }
    }
}
