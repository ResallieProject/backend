using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Resallie.Models;

namespace Resallie.Models
{
    public class Category : Model
    {
        [StringLength(128)]
        [Required] public string Name { get; set; }
        [StringLength(256)]
        [Required] public string Description { get; set; }
        
        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }

    }
}