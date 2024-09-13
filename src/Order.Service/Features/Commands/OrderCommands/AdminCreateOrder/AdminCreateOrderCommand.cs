using MediatR;

namespace Order.Service.Features.Commands.OrderCommands.AdminCreateOrder;

public class AdminCreateOrderCommand : IRequest<AdminCreateOrderResponse>
{
    public AdminCreateOrderCommand(AdminCreateOrderRequest payload)
    {
        Payload = payload;
    }

    public AdminCreateOrderRequest Payload { get; }
}