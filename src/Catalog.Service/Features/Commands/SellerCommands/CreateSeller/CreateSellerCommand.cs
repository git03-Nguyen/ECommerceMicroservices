using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerCommand : IRequest
{
    public CreateSellerCommand(IAccountCreated payload)
    {
        Payload = payload;
    }

    public IAccountCreated Payload { get; }
}