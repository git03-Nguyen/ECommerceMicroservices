namespace ApiGateway.Options;

public class AuthOptions
{
    public const string Name = "Authentication";
    public string ProviderKey { get; set; } = string.Empty;
    public string Authority { get; set; } = string.Empty;
}