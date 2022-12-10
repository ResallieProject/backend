using System.ComponentModel.DataAnnotations;

namespace Resallie.Requests.Authentication;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}