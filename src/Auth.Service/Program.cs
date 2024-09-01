using Auth.Service.Configurations;
using Auth.Service.Data;
using Auth.Service.Data.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Add IdentityServer 4
        const string openIdConfigUrl = "http://localhost:6100/.well-known/openid-configuration";
        Console.WriteLine($"Fetching OpenID configuration from {openIdConfigUrl}");

        string connectionString = builder.Configuration.GetConnectionString("AuthDb");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
        builder.Services.AddIdentityServer()
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddTestUsers(Config.TestUsers)
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<ApplicationUser>();
        builder.Services.AddTransient<AuthConfiguration>();
        
        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI(setup =>
            setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API v1")
        );

        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();
        
        app.UseIdentityServer();

        app.MapControllers();

        app.Run();
    }
}