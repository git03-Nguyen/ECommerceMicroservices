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
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
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

    public static IServiceCollection ConfigureOcelot(this IServiceCollection services,
        IConfigurationManager configuration, IWebHostEnvironment environment)
    {
        var ocelotOptions = configuration.GetSection(OcelotOptions.Name).Get<OcelotOptions>();

        configuration.AddOcelotWithSwaggerSupport(options =>
        {
            options.Folder = ocelotOptions?.Folder;
        });
        configuration.AddJsonFile("ocelot.json", optional: true, reloadOnChange: true);

        services.AddOcelot(configuration)
            .AddCacheManager(x => { x.WithDictionaryHandle(); });
        
        services.AddSwaggerForOcelot(configuration);

        return services;
    }
    
    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }

    public static IServiceCollection ConfigureSwaggerSupport(this IServiceCollection services, IConfiguration configuration,IWebHostEnvironment environment)
    {
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
            app.UseSwaggerForOcelotUI();
        }

        app.UseOcelot();

        return app;
        
    }
}