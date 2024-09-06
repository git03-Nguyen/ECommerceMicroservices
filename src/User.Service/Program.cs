using User.Service.Extensions;

namespace User.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddCustomMassTransitRegistration(builder.Configuration, typeof(Program).Assembly);
        builder.Services.AddDbContextService(builder.Configuration);
        builder.Services.AddMediatRService();
        builder.Services.AddFluentValidationService();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerService(builder.Environment);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwaggerService(builder.Environment);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}