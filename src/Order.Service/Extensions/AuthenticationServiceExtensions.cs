using Contracts.Extensions;
using Contracts.Services.Identity;
using IdentityServer4.AccessTokenValidation;
using Order.Service.Options;

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

        services.AddCustomAuthorizationPolicies();

        return services;
    }
}