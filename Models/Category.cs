using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resallie.Models
{
    public class Category : Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId
        {
            get => Id;
            set => Id = value;
        }

        [Required] public string CategoryName { get; set; }
        [Required] public string CategoryDescription { get; set; }
    }
}
