using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    
    public SignUpResponse(IdentityUser<Guid> user)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
    }
}