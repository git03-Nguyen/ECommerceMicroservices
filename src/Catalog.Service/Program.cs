using Catalog.Service.Data.DbContexts;
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

        #region Authentication and Authorization

        var authority = builder.Configuration["Authentication:Authority"];
        builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authority;
                options.ApiName = "catalog_api";
                options.LegacyAudienceValidation = true;
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly",
                policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim("role", "admin") ||
                               context.User.HasClaim("client_id", "cred.client");
                    });
                });
        });

        #endregion

        builder.Services.AddControllers();
        builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
        builder.Services.AddProblemDetails();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #region MediatR and FluentValidation

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        #endregion

        #region DbContexts and Repository

        builder.Services.AddDbContext<CatalogDbContext>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        #endregion

        #region Swagger dashboard

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = @"Please enter your token with this format: ""Bearer {your token}""",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        #endregion Swagger dashboard

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();

        app.Run();
    }
}