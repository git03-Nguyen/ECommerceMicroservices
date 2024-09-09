using Order.Service.Models.Dtos;

namespace Order.Service.Features.Queries.AdminGetAllOrders;

public class AdminGetAllOrdersResponse
{
    public IEnumerable<OrderDto> Payload { get; set; }
    
    public AdminGetAllOrdersResponse(IEnumerable<Data.Models.Order> orders)
    {
        Payload = orders.Select(order => new OrderDto(order));
    }
}