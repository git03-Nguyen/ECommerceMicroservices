using System.Text;
using Customer.Service.Data;
using Customer.Service.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Customer.Service;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        #region Authentication

        var key = Encoding.ASCII.GetBytes(
            "This is my test private key. This is my test private key. This is my test private key.");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:6100";
                // options.Audience = ???;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        #endregion

        #region Authorization

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "customerClient"));
        });

        #endregion

        builder.Services.AddControllers();

        builder.Services.AddDbContext<CustomerContext>();

        // Add MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Validation.ValidationPipelineBehavior<,>));
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


        Console.WriteLine(builder.Configuration.GetConnectionString("CustomerDb"));
        // Add repositories PostgreSQL
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

        #region Swagger dashboard

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = @"Please enter your token with this format: ""Bearer {your token}""",
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        #endregion Swagger dashboard

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}