using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MMLib.Ocelot.Provider.AppConfiguration;
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

        #region Authentication
        const string authenticationProviderKey = "IdentityApiKey";
        const string identityServerUrl = "https://localhost:6100";
        builder.Services.AddAuthentication(authenticationProviderKey)
            .AddJwtBearer(authenticationProviderKey, options =>
            {
                options.Authority = identityServerUrl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
        #endregion

        #region Ocelot
        builder.Configuration.AddOcelotWithSwaggerSupport(options => { options.Folder = "Configuration/localhost"; });
        builder.Services.AddOcelot(builder.Configuration)
            .AddAppConfiguration()
            .AddCacheManager(x => { x.WithDictionaryHandle(); });
        builder.Services.AddSwaggerForOcelot(builder.Configuration);
        # endregion

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        
        #region Ocelot

        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        
        app.UseSwaggerForOcelotUI(opt => { opt.PathToSwaggerGenerator = "/swagger/docs"; });
        app.UseOcelot().Wait();

        #endregion

        app.Run();
    }
}