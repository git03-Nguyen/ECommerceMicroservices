using ApiGateway.Extensions;

namespace ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.

        builder.Services
            .ConfigureAuthentication(builder.Configuration)
            .ConfigureOcelot(builder.Configuration, builder.Environment)
            .ConfigureControllers()
            .ConfigureSwaggerSupport(builder.Configuration, builder.Environment);

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.UseOcelotService(builder.Environment);

        app.Run();
    }
}