using ApiGateway.Extensions;
using ApiGateway.Options;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddAuthenticationService(builder.Configuration)
            .AddOcelotService(builder.Configuration, builder.Environment)
            .AddSwaggerService(builder.Environment);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseAuthentication();
        app.UseOcelotService(app.Environment);
        app.Run();
    }
}