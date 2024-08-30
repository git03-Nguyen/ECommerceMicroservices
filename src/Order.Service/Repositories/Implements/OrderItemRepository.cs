using Order.Service.Data.DbContexts;
using Order.Service.Data.Models;
using Order.Service.Repositories.Interfaces;

namespace Order.Service.Repositories.Implements;

public class OrderItemRepository : GenericRepositoy<OrderItem, OrderDbContext>, IOrderItemRepository
{
    public OrderItemRepository(OrderDbContext context) : base(context)
    {
    }
}