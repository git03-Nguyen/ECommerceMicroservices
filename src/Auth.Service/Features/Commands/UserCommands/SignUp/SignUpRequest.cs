using Contracts.Constants;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpRequest
{
    public required string UserName { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Role { get; set; } = ApplicationRoleConstants.Customer;

    // public IFormFile? AvatarUpload { get; set; }
    // public string Avatar { get; set; } = string.Empty;
}