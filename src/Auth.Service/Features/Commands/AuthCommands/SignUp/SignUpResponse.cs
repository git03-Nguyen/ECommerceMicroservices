using Auth.Service.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.AuthCommands.SignUp;

public class SignUpResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
    
    public SignUpResponse(IdentityUser<Guid> user, IEnumerable<string> roles)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Roles = roles.ToArray();
    }
}