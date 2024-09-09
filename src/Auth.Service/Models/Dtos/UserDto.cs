using Auth.Service.Data.Models;

namespace Auth.Service.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    
    public UserDto(ApplicationUser user, string? role)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Role = role;
    }
}