using User.Service.Data.DbContexts;
using User.Service.Options;
using User.Service.Repositories;
using User.Service.Repositories.Implements;
using User.Service.Repositories.Interfaces;

namespace User.Service.Extensions;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.Configure<UserDbOptions>(configuration.GetSection(UserDbOptions.Name));
        services.AddDbContext<UserDbContext>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    
}