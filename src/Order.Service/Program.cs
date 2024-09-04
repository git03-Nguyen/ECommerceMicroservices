using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;
using Order.Service.Data.DbContexts;
using Order.Service.Extensions;
using Order.Service.Repositories;
using Order.Service.Repositories.Implements;
using Order.Service.Repositories.Interfaces;

namespace Order.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddAuthorizationService();
        builder.Services.AddCustomMassTransitRegistration(builder.Configuration, typeof(Program).Assembly);
        builder.Services.AddControllers();

        builder.Services.AddMediatRService();
        builder.Services.AddFluentValidationService();
        builder.Services.AddDbContextService(builder.Configuration);

        builder.Services.AddSwaggerService(builder.Environment);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwaggerService(builder.Environment);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}