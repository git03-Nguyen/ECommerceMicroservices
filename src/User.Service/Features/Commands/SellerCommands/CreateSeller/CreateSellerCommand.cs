using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace User.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerCommand : IRequest<bool>
{
    public CreateSellerCommand(AccountCreated payload)
    {
        Payload = payload;
    }

    public AccountCreated Payload { get; set; }
}