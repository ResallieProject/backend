//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace Resallie.Models
//{
//    public class UserAddresses : Model
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int CategoryId
//        {
//            get => Id;
//            set => Id = value;
//        }

//        [Required] public string StreetName { get; set; }
//        [Required] public string ZipCode { get; set; }
//        [Required] public string HouseNumber { get; set; }
//        [Required] public string City { get; set; }
//        [Required] public bool IsPrimary { get; set; }
//    }
//}
