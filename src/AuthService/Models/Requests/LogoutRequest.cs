namespace AuthService.Models.Requests;

public class LogoutRequest
{
    public string RefreshToken { get; set; }
}