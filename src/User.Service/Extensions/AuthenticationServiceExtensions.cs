using IdentityServer4.AccessTokenValidation;
using User.Service.Options;
using User.Service.Services.Identity;

namespace User.Service.Extensions;

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
                options.ApiName = "user_api";
                options.LegacyAudienceValidation = true;
                options.RequireHttpsMetadata = false;
            });
        
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IIdentityService, IdentityService>();

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