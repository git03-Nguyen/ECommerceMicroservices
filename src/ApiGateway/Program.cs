using ApiGateway.Extensions;

namespace ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddOcelotService(builder.Configuration, builder.Environment);
        builder.Services.AddSwaggerService(builder.Environment);
        builder.Services.ConfigureCors(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCorsService();
        app.UseAuthentication();
        app.UseOcelotService(app.Environment);
        app.Run();
    }
}