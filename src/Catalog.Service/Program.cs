using Catalog.Service.Data.DbContexts;
using Catalog.Service.Extensions;
using Catalog.Service.Middlewares;
using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Implements;
using Catalog.Service.Repositories.Interfaces;
using Catalog.Service.Validation;
using FluentValidation;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.OpenApi.Models;

namespace Catalog.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // AddAsync services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddAuthorizationService();
        builder.Services.AddControllers();
        builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
        builder.Services.AddProblemDetails();
        builder.Services.AddMediatRService();
        builder.Services.AddFluentValidationService();
        builder.Services.AddDbContextService(builder.Configuration);
        builder.Services.AddSwaggerService(builder.Environment);

        var app = builder.Build();

        app.UseSwaggerService(builder.Environment);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();

        app.Run();
    }
}