using Catalog.Service.Data.DbContexts;
using Catalog.Service.Data.Models;
using Catalog.Service.Repositories.Interfaces;

namespace Catalog.Service.Repositories.Implements;

public class ProductRepository : GenericRepository<Product, CatalogDbContext>, IProductRepository
{
    public ProductRepository(CatalogDbContext context) : base(context)
    {
    }

    public IQueryable<Product> GetPriceAndStock(IEnumerable<int> productIds,
        CancellationToken cancellationToken = default)
    {
        return _dbSet.Where(p => productIds.Contains(p.ProductId))
            .Select(p => new Product
            {
                ProductId = p.ProductId,
                Price = p.Price,
                Stock = p.Stock
            });
    }
}