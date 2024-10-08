using Auth.Service.Data.Models;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpResponse
{
    public SignUpResponse(ApplicationUser user, string role)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Role = role;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}