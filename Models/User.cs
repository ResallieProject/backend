using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Resallie.Models;

public class User : Model
{
    [StringLength(128)]
    [Required]
    public string Email { get; set; }
    
    [JsonIgnore]
    [StringLength(512)]
    [Required]
    public string Password { get; set; }
    
    [StringLength(32)]
    [Required]
    public string FirstName { get; set; }
    
    [StringLength(64)]
    [Required]
    public string LastName { get; set; }
    
    [StringLength(32)]
    public string? Gender { get; set; }
    
    [Required]
    [StringLength(32)]
    public string Phone { get; set; }

    [Timestamp]
    public DateTime? EmailVerifiedAt { get; set; }
}