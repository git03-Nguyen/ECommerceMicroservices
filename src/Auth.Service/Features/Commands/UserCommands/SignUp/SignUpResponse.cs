using Auth.Service.Data.Models;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpResponse
{
    public SignUpResponse(ApplicationUser user, IEnumerable<string> roles)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Roles = roles;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}