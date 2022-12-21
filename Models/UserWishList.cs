using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resallie.Models
{
    public class UserWishList : Model
    {
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [ForeignKey("Advertisement")]
        public int AdvertisementId { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
