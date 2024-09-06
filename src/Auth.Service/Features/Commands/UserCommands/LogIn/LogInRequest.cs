namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}