using Auth.Service.Configurations;
using Auth.Service.Data.DbContexts;
using Auth.Service.Data.Models;
using Auth.Service.Options;
using Auth.Service.Services.Identity;
using Auth.Service.Services.Profile;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        // Add database context
        services.Configure<AuthDbOptions>(configuration.GetSection(AuthDbOptions.Name));
        services.AddDbContext<ApplicationDbContext>();

        // Add Identity
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.AddTransient<IIdentityService, IdentityService>();

        // Add IdentityServer 4
        services.AddIdentityServer()
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddTestUsers(Config.TestUsers)
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<CustomProfileService>();
        
        // Add AuthOptions
        services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.Name));

        return services;
    }
    
    
}