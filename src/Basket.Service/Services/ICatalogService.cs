namespace Basket.Service.Services;

public interface ICatalogService
{
    Task<CatalogService.GetProductByIdResponse> GetProductById(int productId);
    Task<CatalogService.GetProductsByIdsResponse> GetProductsByIds(IEnumerable<int> productIds);
}