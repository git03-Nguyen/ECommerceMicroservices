using ApiGateway.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiGateway.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var authConfiguration = configuration.GetSection(AuthOptions.Name).Get<AuthOptions>();

        var authenticationProviderKey = authConfiguration?.ProviderKey;
        var authority = authConfiguration?.Authority;

        if (authenticationProviderKey != null)
            services.AddAuthentication()
                .AddJwtBearer(authenticationProviderKey, options =>
                {
                    options.Authority = authority;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

        return services;
    }
}