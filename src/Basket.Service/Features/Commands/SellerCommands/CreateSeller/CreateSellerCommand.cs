using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerCommand : IRequest
{
    public CreateSellerCommand(IAccountCreated payload)
    {
        Payload = payload;
    }

    public IAccountCreated Payload { get; }
}