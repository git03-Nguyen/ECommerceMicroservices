using System.Text.Json;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInResponse
{
    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    // public string RefreshToken { get; set; }
    public int ExpiresIn { get; set; }
    public string[] Scopes { get; set; }

    public LogInResponse(string responseContent)
    {
        Console.WriteLine(responseContent);
        var response = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseContent);
        AccessToken = response?["access_token"].GetString();
        TokenType = response["token_type"].GetString();
        // RefreshToken = response["refresh_token"].GetString();
        ExpiresIn = response["expires_in"].GetInt32();
        Scopes = response["scope"].GetString().Split(' ');
    }
}