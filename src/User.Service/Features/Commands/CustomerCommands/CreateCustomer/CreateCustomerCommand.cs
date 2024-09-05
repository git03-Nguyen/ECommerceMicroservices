using Contracts.MassTransit.Events;
using MediatR;

namespace User.Service.Features.Commands.CustomerCommands.CreateCustomer;

public class CreateCustomerCommand : IRequest<bool>
{
    public NewAccountCreated Payload { get; set; }
    
    public CreateCustomerCommand(NewAccountCreated payload)
    {
        Payload = payload;
    }
    
}