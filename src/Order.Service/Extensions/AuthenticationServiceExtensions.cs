using IdentityServer4.AccessTokenValidation;
using Order.Service.Options;
using Order.Service.Services.Identity;

namespace Order.Service.Extensions;

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
                options.ApiName = "order_api";
                options.LegacyAudienceValidation = true;
                options.RequireHttpsMetadata = false;
            });

        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
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