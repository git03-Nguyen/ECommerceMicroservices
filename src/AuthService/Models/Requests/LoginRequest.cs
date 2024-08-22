namespace AuthService.Models.Requests;

public class LoginRequest
{
    public string Account { get; set; }
    public string Password { get; set; }
}