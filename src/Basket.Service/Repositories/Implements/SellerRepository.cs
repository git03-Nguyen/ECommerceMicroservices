using Basket.Service.Data.DbContexts;
using Basket.Service.Data.Models;
using Basket.Service.Repositories.Interfaces;

namespace Basket.Service.Repositories.Implements;

public class SellerRepository : GenericRepository<Seller, BasketDbContext>, ISellerRepository
{
    public SellerRepository(BasketDbContext context) : base(context)
    {
    }
}