using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.OpenApi.Writers;

namespace Auth.Service;

public static class Config
{
    // ApiResources define the apis in your system
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("account_api", "Account API"),
        new ApiResource("basket_api", "Basket API"),
        new ApiResource("catalog_api", "Catalog API"),
        new ApiResource("order_api", "Order API")
    ];    
    
    // ApiScope is used to protect the API 
    //The effect is the same as that of API resources in IdentityServer 3.x
    // The difference is that API resources are more detailed and can be used to define the API's user claims
    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("account_api", "Account API"),
        new ApiScope("basket_api", "Basket API"),
        new ApiScope("catalog_api", "Catalog API"),
        new ApiScope("order_api", "Order API")
    ];
    
    // Identity resources are data like user ID, name, or email address of a user
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResource("roles", "Roles", new[] {JwtClaimTypes.Role})
    ];
    
    // Clients are applications that can access your resources, such as web applications, mobile apps, or microservices
    public static IEnumerable<Client> Clients =>
    [
        // Postman testing
        new Client
        {
            ClientId = "postman",
            ClientName = "Postman Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            // Scopes that client has access to catalog_api, order_api, basket_api, account_api
            AllowedScopes = { "catalog_api", "order_api", "basket_api", "account_api" }
        },
        
        new Client()
        {
            ClientId = "postman-code",
            ClientName = "Postman Client Code",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                "roles"
            },
            AllowedCorsOrigins = { "https://www.getpostman.com" },
            AllowOfflineAccess = true,
            AccessTokenLifetime = 3600,
            RefreshTokenUsage = TokenUsage.ReUse,
            RefreshTokenExpiration = TokenExpiration.Sliding,
            UpdateAccessTokenClaimsOnRefresh = true
        },
        
        // Swagger UI: Catalog.Service
        new Client
        {
            ClientId = "catalogswaggerui",
            ClientName = "Catalog Swagger UI",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,

            RedirectUris = { $"https://localhost:6300/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"https://localhost:6300/swagger/" },

            AllowedScopes =
            {
                "catalog_api"
            }
        },
        
        // Other clients...
        
    ];
    
    // Test users for development
    public static List<TestUser> TestUsers => new()
    {
        new()
        {
            SubjectId = "1",
            Username = "admin",
            Password = "admin",
            Claims = new List<Claim>
            {
                new(JwtClaimTypes.GivenName, "Anh"),
                new(JwtClaimTypes.FamilyName, "Nguyen Dinh")
            }
        }
    };
}