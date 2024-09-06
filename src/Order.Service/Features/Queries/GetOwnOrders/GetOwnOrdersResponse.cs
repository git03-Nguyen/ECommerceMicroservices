using Order.Service.Models.Dtos;

namespace Order.Service.Features.Queries.GetOwnOrders;

public class GetOwnOrdersResponse
{
    public IEnumerable<OrderDto> Payload { get; set; }
    
    public GetOwnOrdersResponse(IEnumerable<Data.Models.Order> orders)
    {
        Payload = orders.Select(order => new OrderDto(order));
    }
}