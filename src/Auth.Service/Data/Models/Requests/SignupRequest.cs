namespace Auth.Service.Data.Models.Requests;

public class SignupRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}