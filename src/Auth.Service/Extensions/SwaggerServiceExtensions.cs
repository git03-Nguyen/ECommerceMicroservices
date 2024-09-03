namespace Auth.Service.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        if (!environment.IsProduction())
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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