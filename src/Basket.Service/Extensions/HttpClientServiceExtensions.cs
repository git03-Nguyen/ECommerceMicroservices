using Basket.Service.Services;

namespace Basket.Service.Extensions;

public static class HttpClientServiceExtensions
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services,
        IConfigurationManager configuration)
    {
        services.AddHttpClient<CatalogService>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("ApiOptions:Catalog:BaseAddress").Value);
        });

        return services;
    }
}