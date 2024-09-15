using Basket.Service.Extensions;
using Basket.Service.Middlewares;

namespace Basket.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddCustomMassTransitRegistration(builder.Configuration, typeof(Program).Assembly);
        builder.Services.AddFluentValidationService();
        builder.Services.AddMediatRService();
        builder.Services.AddHttpClientServices(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddDbContextService(builder.Configuration);
        builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
        builder.Services.AddProblemDetails();

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