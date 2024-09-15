using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MediatR;

namespace Basket.Service.Features.Commands.BasketCommands.DeleteBasket;

public class DeleteBasketCommand : IRequest
{
    public DeleteBasketCommand(IAccountDeleted payload)
    {
        Payload = payload;
    }

    public IAccountDeleted Payload { get; set; }
}