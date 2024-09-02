
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        var services = builder.Services;
        var configuration = builder.Configuration;

        // Authentication
        // var authConfiguration = configuration.GetSection(AuthOptions.Name).Get<AuthOptions>();
        // var authenticationProviderKey = authConfiguration?.ProviderKey;
        // var authority = authConfiguration?.Authority;
        
        var authenticationProviderKey = "IdentityApiKey";
        var authority = "http://localhost:6100";

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
            
        
        // Ocelot
        builder.Services.AddOcelot(builder.Configuration);
        builder.Configuration.AddJsonFile("ocelot.json");
        
        // builder.Services
        //     .ConfigureAuthentication(builder.Configuration)
        //     .ConfigureOcelot(builder.Configuration, builder.Environment)
        //     .ConfigureControllers()
        //     .ConfigureSwaggerSupport(builder.Configuration, builder.Environment);

        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(_ => { });

        // app.MapControllers();
        // app.UseOcelotService(builder.Environment);
        app.UseOcelot();

        app.Run();
    }
}