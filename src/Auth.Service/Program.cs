namespace Auth.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Add IdentityServer 4
        const string openIdConfigUrl = "https://localhost:6100/.well-known/openid-configuration";
        Console.WriteLine($"Fetching OpenID configuration from {openIdConfigUrl}");
        builder.Services.AddIdentityServer()
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddTestUsers(Config.TestUsers)
            .AddDeveloperSigningCredential();
        
        builder.Services.AddControllers();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API v1")
            );
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();
        app.UseIdentityServer();

        app.MapControllers();

        app.Run();
    }
}