namespace Auth.Service.Features.Commands.AuthCommands.SignUp;

public class SignupRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] Roles { get; set; }
}