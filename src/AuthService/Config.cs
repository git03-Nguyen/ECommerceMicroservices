using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AuthService;

public static class Config
{
    public static IEnumerable<Client> Clients => new Client[]
    {
        new Client()
        {
            ClientId = "customerClient",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = { "customerAPI" },
            AccessTokenLifetime = 3600
        }
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new ApiScope("customerAPI", "Customer API")
    };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("customerAPI", "Customer API")
            {
                Scopes = { "customerAPI" }
            }
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    public static List<TestUser> TestUsers => new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "anh",
            Password = "password",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.GivenName, "Anh"),
                new Claim(JwtClaimTypes.FamilyName, "Nguyen Dinh")
            }
        }
    };
}