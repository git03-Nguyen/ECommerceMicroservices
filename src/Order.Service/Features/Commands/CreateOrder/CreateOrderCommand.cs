using MediatR;

namespace Order.Service.Features.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderResponse>
{
    public CreateOrderCommand(CreateOrderRequest request)
    {
        Request = request;
    }

    public CreateOrderRequest Request { get; }
}