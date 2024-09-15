using Order.Service.Models.Dtos;

namespace Order.Service.Features.Queries.OrderQueries.AdminGetAllOrders;

public class AdminGetAllOrdersResponse
{
    public AdminGetAllOrdersResponse(IEnumerable<Data.Models.Order> orders)
    {
        Payload = orders.Select(order => new OrderDto(order));
    }

    public IEnumerable<OrderDto> Payload { get; set; }
}