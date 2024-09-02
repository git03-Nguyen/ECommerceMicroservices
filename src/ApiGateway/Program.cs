using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Configuration.AddOcelot("Routes/localhost", builder.Environment, MergeOcelotJson.ToFile,
            optional: false, reloadOnChange: true);
        builder.Services.AddOcelot()
            .AddCacheManager(x => x.WithDictionaryHandle());
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.UseOcelot().Wait();
        app.Run();
    }
}