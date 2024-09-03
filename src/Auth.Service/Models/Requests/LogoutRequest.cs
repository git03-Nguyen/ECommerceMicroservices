namespace Auth.Service.Models.Requests;

public class LogoutRequest
{
    public string RefreshToken { get; set; }
}