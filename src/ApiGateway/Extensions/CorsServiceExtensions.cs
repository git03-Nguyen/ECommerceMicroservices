namespace ApiGateway.Extensions;

public static class CorsServiceExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }
    
    public static IApplicationBuilder UseCorsService(this IApplicationBuilder app)
    {
        app.UseCors("CorsPolicy");
        return app;
    }
}