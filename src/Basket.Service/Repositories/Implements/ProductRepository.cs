using Basket.Service.Data.DbContexts;
using Basket.Service.Data.Models;
using Basket.Service.Repositories.Interfaces;

namespace Basket.Service.Repositories.Implements;

public class ProductRepository : GenericRepository<Product, BasketDbContext>, IProductRepository
{
    public ProductRepository(BasketDbContext context) : base(context)
    {
    }
}