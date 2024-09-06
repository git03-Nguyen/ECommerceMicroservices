using Order.Service.Models.Dtos;

namespace Order.Service.Features.Commands.AdminCreateOrder;

public class AdminCreateOrderResponse
{
    public AdminCreateOrderResponse(Data.Models.Order order)
    {
        Payload = new OrderDto(order);
    }

    public OrderDto Payload { get; set; }
}