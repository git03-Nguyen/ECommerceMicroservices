using Basket.Service.Data;
using Basket.Service.Extensions;
using Basket.Service.Repositories;
using Basket.Service.Repositories.Implements;
using Basket.Service.Repositories.Interfaces;
using FluentValidation;
using IdentityServer4.AccessTokenValidation;
using Microsoft.OpenApi.Models;

namespace Basket.Service;

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

        app.UseSwaggerService(builder.Environment);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}