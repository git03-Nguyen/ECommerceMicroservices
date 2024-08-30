using Order.Service.Data.DbContexts;
using Order.Service.Repositories.Interfaces;

namespace Order.Service.Repositories.Implements;

public class OrderRepository : GenericRepositoy<Data.Models.Order, OrderDbContext>, IOrderRepository
{
    public OrderRepository(OrderDbContext context) : base(context)
    {
    }
}