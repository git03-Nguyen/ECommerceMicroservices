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
        new ApiResource("basket_api", "Basket API"),
        new ApiResource("catalog_api", "Catalog API"),
        new ApiResource("order_api", "Order API")
    ];    
    
    // Identity resources are data like user ID, name, or email address of a user
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Address(),
        new IdentityResources.Email(),
        new IdentityResource("roles", "Roles", new[] {JwtClaimTypes.Role})
    ];
    
    // Clients are applications that can access your resources, such as web applications, mobile apps, or microservices
    public static IEnumerable<Client> Clients =>
    [
        new Client
        {
            ClientId = "basket_client",
            ClientName = "Basket Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials, // Client Credentials Flow
            ClientSecrets = {new Secret("secret".Sha256())},
            AllowOfflineAccess = true,
            AllowedScopes = {"basket_api"}
        },
        
        new Client
        {
            ClientId = "catalog_client",
            AllowedGrantTypes = GrantTypes.ClientCredentials, // Client Credentials Flow
            ClientSecrets = {new Secret("secret".Sha256())},
            AllowedScopes = {"catalog_api"}
        },
        
        new Client
        {
            ClientId = "order_client",
            AllowedGrantTypes = GrantTypes.ClientCredentials, // Client Credentials Flow
            ClientSecrets = {new Secret("secret".Sha256())},
            AllowedScopes = {"order_api"}
        }
        
        // Other microservices and clients...
        
    ];

    // ApiScopes define the scope of the APIs
    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("basket_api", "Basket API"),
        new ApiScope("catalog_api", "Catalog API"),
        new ApiScope("order_api", "Order API")
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