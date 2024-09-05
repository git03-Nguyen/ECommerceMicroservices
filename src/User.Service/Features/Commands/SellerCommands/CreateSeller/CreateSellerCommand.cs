using Contracts.MassTransit.Events;
using MediatR;

namespace User.Service.Features.Commands.SellerCommands.CreateSeller;

public class CreateSellerCommand : IRequest<bool>
{
    public NewAccountCreated Payload { get; set; }
    
    public CreateSellerCommand(NewAccountCreated payload)
    {
        Payload = payload;
    }
    
}