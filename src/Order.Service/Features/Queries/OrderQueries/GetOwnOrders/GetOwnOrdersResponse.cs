using Order.Service.Models.Dtos;

namespace Order.Service.Features.Queries.OrderQueries.GetOwnOrders;

public class GetOwnOrdersResponse
{
    public GetOwnOrdersResponse(IEnumerable<Data.Models.Order> orders)
    {
        Payload = orders.Select(order => new OrderDto(order));
    }

    public IEnumerable<OrderDto> Payload { get; set; }
}