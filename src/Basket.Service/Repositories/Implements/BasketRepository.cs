using Basket.Service.Data.DbContexts;
using Basket.Service.Repositories.Interfaces;

namespace Basket.Service.Repositories.Implements;

public class BasketRepository : GenericRepository<Data.Models.Basket, BasketDbContext>, IBasketRepository
{
    public BasketRepository(BasketDbContext context) : base(context)
    {
    }
}