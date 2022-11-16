using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resallie.Models
{
    public class User : Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId
        {
            get => Id;
            set => Id = value;
        }

        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public string Phonenumber { get; set; }
        [Required] public DateTime EmailVeriviedAt { get; set; }
    }
}
