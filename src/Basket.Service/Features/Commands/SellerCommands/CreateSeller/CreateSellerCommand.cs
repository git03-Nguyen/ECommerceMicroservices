using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerCommand : IRequest
{
    public IAccountCreated Payload { get; }
    
    public CreateSellerCommand(IAccountCreated payload)
    {
        Payload = payload;
    }
    
}