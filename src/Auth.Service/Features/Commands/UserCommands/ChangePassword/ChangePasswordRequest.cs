namespace Auth.Service.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordRequest
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
}