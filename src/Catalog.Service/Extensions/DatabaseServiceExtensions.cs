using Catalog.Service.Data.DbContexts;
using Catalog.Service.Options;
using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Implements;
using Catalog.Service.Repositories.Interfaces;

namespace Catalog.Service.Extensions;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.Name));
        services.AddDbContext<CatalogDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}