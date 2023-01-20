using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resallie.Data;
using Bogus;


namespace Resallie.Models
{
    public class Category : Model
    {
        #region Attributen
        [StringLength(128)]
        [Required] public string Name { get; set; }
        [StringLength(256)]
        [Required] public string Description { get; set; }
        
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        #endregion
    }
}