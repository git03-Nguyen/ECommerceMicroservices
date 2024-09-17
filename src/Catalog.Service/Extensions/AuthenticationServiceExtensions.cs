using Catalog.Service.Options;
using Contracts.Extensions;
using Contracts.Services.Identity;
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

        services.AddCustomAuthorizationPolicies();

        return services;
    }
}