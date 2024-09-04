using Auth.Service.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    
    public SignUpResponse(ApplicationUser user, IEnumerable<string> roles)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        FullName = user.FullName;
        Roles = roles;
    }
}