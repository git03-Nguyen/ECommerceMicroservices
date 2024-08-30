namespace Auth.Service.Configurations;

public class AuthConfiguration
{
    public string GrantType { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
    public string IdentityServerUrl { get; set; }
    public string IdentityServerTokenEndpoint { get; set; }
    
    public AuthConfiguration(IConfiguration configuration)
    {
        GrantType = configuration["Authentication:GrantType"];
        ClientId = configuration["Authentication:ClientId"];
        ClientSecret = configuration["Authentication:ClientSecret"];
        Scope = configuration["Authentication:Scope"];
        IdentityServerUrl = configuration["Authentication:IdentityServerUrl"];
        IdentityServerTokenEndpoint = configuration["Authentication:IdentityServerTokenEndpoint"];
    }
    
}