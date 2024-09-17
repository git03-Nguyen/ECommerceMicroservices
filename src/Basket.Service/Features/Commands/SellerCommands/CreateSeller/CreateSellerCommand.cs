using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerCommand : IRequest
{
    public CreateSellerCommand(ISellerCreated payload)
    {
        Payload = payload;
    }

    public ISellerCreated Payload { get; }
}