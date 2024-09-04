using Basket.Service.Data;
using Basket.Service.Options;
using Basket.Service.Repositories;
using Basket.Service.Repositories.Implements;
using Basket.Service.Repositories.Interfaces;

namespace Basket.Service.Extensions;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.Configure<BasketDbOptions>(configuration.GetSection(BasketDbOptions.Name));
        services.AddDbContext<BasketDbContext>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    
}