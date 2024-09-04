using Catalog.Service.Data.Models;

namespace Catalog.Service.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    // Get only price and stock of the products
    IQueryable<Product> GetPriceAndStock(IEnumerable<int> productIds, CancellationToken cancellationToken = default);
}