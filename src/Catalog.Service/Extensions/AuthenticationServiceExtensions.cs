using Catalog.Service.Options;
using Catalog.Service.Services.Identity;
using IdentityServer4.AccessTokenValidation;

namespace Catalog.Service.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var authOptions = configuration.GetSection(AuthOptions.Name).Get<AuthOptions>();
        var authority = authOptions?.Authority;
        
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authority;
                options.ApiName = "catalog_api";
                options.LegacyAudienceValidation = true;
                options.RequireHttpsMetadata = false;
            });

        services.AddHttpContextAccessor();
        services.AddTransient<IIdentityService, IdentityService>();

        return services;
    }
    
    public static IServiceCollection AddAuthorizationService(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly",
                policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim("role", "admin") ||
                               context.User.HasClaim("client_id", "cred.client");
                    });
                });
        });

        return services;
    }
    
}