using Catalog.Service.Extensions;
using Catalog.Service.Middlewares;

namespace Catalog.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // AddAsync services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddAuthorizationService();
        builder.Services.AddCustomMassTransitRegistration(builder.Configuration, typeof(Program).Assembly);
        builder.Services.AddControllers();
        builder.Services.AddMediatRService();
        builder.Services.AddFluentValidationService();
        builder.Services.AddDbContextService(builder.Configuration);
        builder.Services.AddSwaggerService(builder.Environment);
        builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        app.UseStaticFiles();

        app.UseSwaggerService(builder.Environment);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();

        app.Run();
    }
}