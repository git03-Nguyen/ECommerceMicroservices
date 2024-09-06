using Microsoft.OpenApi.Models;

namespace Basket.Service.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
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
        }

        return services;
    }

    public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}