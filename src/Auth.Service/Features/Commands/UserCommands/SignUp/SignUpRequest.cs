using Contracts.Constants;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpRequest
{
    public required string UserName { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public string Role { get; set; } = ApplicationRoleConstants.Customer;
}