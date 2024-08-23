namespace AuthService;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Add IdentityServer 4
        // https://localhost:5005/.well-known/openid-configuration
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