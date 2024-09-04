namespace Auth.Service.Options;

public class AuthOptions
{
    public const string Name = nameof(AuthOptions);
    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
    public string IdentityServerUrl { get; set; }
    public string IdentityServerTokenEndpoint { get; set; }
}