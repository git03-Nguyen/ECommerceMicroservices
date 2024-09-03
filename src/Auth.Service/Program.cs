using Auth.Service.Extensions;

namespace Auth.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthenticationService(builder.Configuration);
        builder.Services.AddControllers();
        builder.Services.AddMediatRService();
        builder.Services.AddFluentValidationService();
        builder.Services.AddSwaggerService(builder.Environment);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwaggerService(builder.Environment);
        
        app.UseRouting();
        app.MapControllers();
        
        app.UseIdentityServer();
        
        app.Run();
    }
}