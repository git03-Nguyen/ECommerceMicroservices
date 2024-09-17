namespace ApiGateway.Extensions;

public static class CorsServiceExtensions
{
    private static string _corsPolicyName = "CorsPolicy";
    
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigin = configuration["AllowedOrigin"] ?? "http://localhost:3000";
        services.AddCors(options =>
        {
            options.AddPolicy(_corsPolicyName, builder =>
            {
                builder
                    .WithOrigins(allowedOrigin)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        return services;
    }

    public static IApplicationBuilder UseCorsService(this IApplicationBuilder app)
    {
        app.UseCors(_corsPolicyName);
        return app;
    }
}