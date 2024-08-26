using Basket.Service.Data;
using Basket.Service.Data.Repositories;
using FluentValidation;
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

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:6100";
                options.Audience = "basket_service";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
            });
        
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "basket_client"));
            options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("role", "admin"));
        });

        #endregion

        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        #region MediatR and FluentValidation
        
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        // builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Validation.ValidationPipelineBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        
        #endregion

        #region DbContext and Repository
        
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddDbContext<BasketDbContext>();
        
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