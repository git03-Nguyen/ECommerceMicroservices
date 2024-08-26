using Microsoft.IdentityModel.Tokens;
using Product.Service.Data;
using Product.Service.Repositories;

namespace Product.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddDbContext<ProductItemContext>();
        
        #region Authentication

        const string authenticationProviderKey = "IdentityApiKey";
        const string identityServerUrl = "https://localhost:6100";
        builder.Services.AddAuthentication(authenticationProviderKey)
            .AddJwtBearer(authenticationProviderKey, options =>
            {
                options.Authority = identityServerUrl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        #endregion

        // Add MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        Console.WriteLine(builder.Configuration.GetConnectionString("ProductItemDb"));
        // Add repositories PostgreSQL
        builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();

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

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}