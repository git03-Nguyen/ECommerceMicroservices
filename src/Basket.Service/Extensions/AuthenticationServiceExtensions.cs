using Basket.Service.Options;
using IdentityServer4.AccessTokenValidation;

namespace Basket.Service.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var authConfiguration = configuration.GetSection(AuthOptions.Name).Get<AuthOptions>();
        var authority = authConfiguration?.Authority;

        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authority;
                options.ApiName = "basket_api";
                options.LegacyAudienceValidation = true;
                options.RequireHttpsMetadata = false;
            });

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