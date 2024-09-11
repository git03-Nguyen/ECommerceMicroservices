using System.Text.Json;
using Auth.Service.Data.Models;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInResponse
{
    public LogInResponse(ApplicationUser user, string responseContent)
    {
        var response = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseContent);
        Token = response?["access_token"].GetString();
        Email = user.Email;
        UserName = user.UserName;
    }

    public string Email { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
    
}