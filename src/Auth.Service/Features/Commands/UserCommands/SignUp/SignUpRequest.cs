using Contracts.Constants;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignupRequest
{
    public required string UserName { get; set; } 
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string FullName { get; set; } = string.Empty;
    public string[] Roles { get; set; } = [ ApplicationRoleConstants.Customer ];
}