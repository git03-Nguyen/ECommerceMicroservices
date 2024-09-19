using Auth.Service.Extensions;
using Auth.Service.Middlewares;
using Auth.Service.SignalRWebpack.Hubs;

namespace Auth.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddSignalR();
        builder.Services.AddHttpClient();
        builder.Services.AddCustomMassTransitRegistration(builder.Configuration, typeof(Program).Assembly);
        builder.Services.AddMediatRService();
        builder.Services.AddFluentValidationService();
        builder.Services.AddSwaggerService(builder.Environment);
        builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwaggerService(builder.Environment);
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseIdentityServer();
        app.UseExceptionHandler();
        app.MapHub<StronglyTypedChatHub>("/Hubs/test");

        app.Run();
    }
}