using Basket.Service.Data;
using Basket.Service.Extensions;
using Basket.Service.Repositories;
using Basket.Service.Repositories.Implements;
using Basket.Service.Repositories.Interfaces;
using Contracts.Masstransit.Core.PublishEndpoint;
using Contracts.Masstransit.Extensions;
using FluentValidation;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Basket.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        #region Authentication and Authorization

        builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:6100";
                options.ApiName = "catalog_api";
                options.LegacyAudienceValidation = true;
            });
        
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return context.User.HasClaim("role", "admin") || context.User.HasClaim("client_id", "cred.client");
                });
            });
        });

        #endregion

        # region MassTransit and RabbitMQ
        
        builder.Services.AddCustomMassTransitRegistration(builder.Configuration, typeof(Program).Assembly);
        
        // For test
        // builder.Services.AddScoped<IPublishEndpointCustomProvider, PublishEndpointCustomProvider>();    
        
        # endregion
        
        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        
        #region MediatR and FluentValidation
        
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        // builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Validation.ValidationPipelineBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        
        #endregion

        #region DbContexts and Repository
        
        builder.Services.AddDbContext<BasketDbContext>();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}