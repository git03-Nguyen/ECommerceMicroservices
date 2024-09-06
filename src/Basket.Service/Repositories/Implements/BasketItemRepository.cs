using Basket.Service.Data.DbContexts;
using Basket.Service.Data.Models;
using Basket.Service.Repositories.Interfaces;

namespace Basket.Service.Repositories.Implements;

public class BasketItemRepository : GenericRepositoy<BasketItem, BasketDbContext>, IBasketItemRepository
{
    public BasketItemRepository(BasketDbContext context) : base(context)
    {
    }
}