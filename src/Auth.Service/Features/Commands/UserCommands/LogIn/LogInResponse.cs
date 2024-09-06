using System.Text.Json;

namespace Auth.Service.Features.Commands.UserCommands.LogIn;

public class LogInResponse
{
    public LogInResponse(string responseContent)
    {
        var response = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseContent);
        AccessToken = response?["access_token"].GetString();
        TokenType = response["token_type"].GetString();
        ExpiresIn = response["expires_in"].GetInt32();
        Scopes = response["scope"].GetString().Split(' ');
    }

    public string AccessToken { get; set; }
    public string TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string[] Scopes { get; set; }
}