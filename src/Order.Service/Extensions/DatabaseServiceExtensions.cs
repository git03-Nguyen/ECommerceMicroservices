using Order.Service.Data.DbContexts;
using Order.Service.Options;
using Order.Service.Repositories;
using Order.Service.Repositories.Implements;
using Order.Service.Repositories.Interfaces;

namespace Order.Service.Extensions;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Name));
        services.AddDbContext<OrderDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}