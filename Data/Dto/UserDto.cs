
using Resallie.Models;

namespace Resallie.Data.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserDto(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
    }
}