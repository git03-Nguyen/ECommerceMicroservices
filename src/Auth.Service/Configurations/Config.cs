using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Auth.Service.Configurations;

public static class Config
{
    // ApiResources define the apis in your system
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new()
        {
            Name = "internal_apis",
            UserClaims =
            {
                JwtClaimTypes.Email,
                JwtClaimTypes.Role
            },
            Enabled = true,
            DisplayName = "Full access API",
            Scopes =
            {
                "account_api", "basket_api", "catalog_api", "order_api", "user_api",
                IdentityServerConstants.StandardScopes.OfflineAccess
            }
        }
    ];

    // ApiScope is used to protect the API 
    //The effect is the same as that of API resources in IdentityServer 3.x
    // The difference is that API resources are more detailed and can be used to define the API's user claims
    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("account_api", "Account API"),
        new("basket_api", "Basket API"),
        new("catalog_api", "Catalog API"),
        new("order_api", "Order API"),
        new("user_api", "User API")
    ];

    // Identity resources are data like user ID, name, or email address of a user
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new("roles", "Roles", new[] { JwtClaimTypes.Role })
    ];

    // Clients are applications that can access your resources, such as web applications, mobile apps, or microservices
    public static IEnumerable<Client> Clients =>
    [
        // Client credentials flow
        new()
        {
            ClientId = "cred.client",
            ClientName = "Credential-Flow Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret".Sha256()) },
            // Scopes that client has access to catalog_api, order_api, basket_api, account_api
            AllowedScopes = { "catalog_api", "order_api", "basket_api", "account_api", "user_api" }
        },

        // Password flow
        new()
        {
            ClientId = "pwd.client",
            ClientName = "Password-Flow Client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AccessTokenLifetime = 604800, // 7 days
            AllowedScopes =
            {
                "catalog_api", "order_api", "basket_api", "account_api", "user_api",
                IdentityServerConstants.StandardScopes.Email
            },
            AllowOfflineAccess = true
        }


        // // resource owner password grant client
        // new Client
        // {
        //     ClientId = "ro.client",
        //     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        //
        //     ClientSecrets = 
        //     {
        //         new Secret("secret".Sha256())
        //     },
        //     AllowedScopes = { "catalog_api", "order_api", "basket_api", "account_api" }
        // },
        //
        // // OpenID Connect hybrid flow and client credentials client (MVC)
        // new Client
        // {
        //     ClientId = "mvc",
        //     ClientName = "MVC Client",
        //     AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
        //
        //     RequireConsent = true,
        //
        //     ClientSecrets = 
        //     {
        //         new Secret("secret".Sha256())
        //     },
        //
        //     RedirectUris = { "http://localhost:6000/signin-oidc" },
        //     PostLogoutRedirectUris = { "http://localhost:6000" },
        //
        //     AllowedScopes =
        //     {
        //         IdentityServerConstants.StandardScopes.OpenId,
        //         IdentityServerConstants.StandardScopes.Profile,
        //         IdentityServerConstants.StandardScopes.OfflineAccess,
        //         "catalog_api",
        //         "order_api",
        //         "basket_api",
        //         "account_api"
        //     }
        // },
        //
        // // Swagger UI: Catalog.Service
        // new Client
        // {
        //     ClientId = "catalogswaggerui",
        //     ClientName = "Catalog Swagger UI",
        //     AllowedGrantTypes = GrantTypes.Implicit,
        //     AllowAccessTokensViaBrowser = true,
        //
        //     RedirectUris = { $"https://localhost:6300/swagger/oauth2-redirect.html" },
        //     PostLogoutRedirectUris = { $"https://localhost:6300/swagger/" },
        //
        //     AllowedScopes =
        //     {
        //         "catalog_api"
        //     }
        // },

        // Other clients...
    ];

    // Test users for development
    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "admin",
            Password = "admin",
            Claims = new List<Claim>
            {
                new(JwtClaimTypes.GivenName, "Anh"),
                new(JwtClaimTypes.FamilyName, "Nguyen Dinh"),
                new(JwtClaimTypes.Email, "email@email.com"),
                new(JwtClaimTypes.Role, "Admin")
            }
        }
    };
}