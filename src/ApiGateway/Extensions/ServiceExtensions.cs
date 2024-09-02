using ApiGateway.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway.Extensions;

public static class ServiceExtensions
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

    public static IServiceCollection AddOcelotService(this IServiceCollection services,
        IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        var ocelotOptions = configuration.GetSection(OcelotOptions.Name).Get<OcelotOptions>();
        configuration.AddOcelot(ocelotOptions?.Folder, environment, MergeOcelotJson.ToFile,
            optional: false, reloadOnChange: true);
        configuration.AddOcelotWithSwaggerSupport(options =>
        {
            options.Folder = ocelotOptions?.Folder;
        });
        
        services.AddOcelot()
            .AddCacheManager(x => x.WithDictionaryHandle());
        services.AddSwaggerForOcelot(configuration);
        
        return services;
    }

    public static IServiceCollection AddSwaggerService(this IServiceCollection services,IWebHostEnvironment environment)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        if (!environment.IsProduction())
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        return services;
    }
    
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        
        return services;
    }
    
    public static IApplicationBuilder UseOcelotService(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerForOcelotUI(options =>
            {
                options.PathToSwaggerGenerator = "/swagger/docs";
            });
        }

        app.UseOcelot();
        
        return app;
    }
}