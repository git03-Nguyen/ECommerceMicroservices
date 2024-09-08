using Contracts.MassTransit.Messages.Events;
using MediatR;

namespace User.Service.Features.Commands.CustomerCommands.CreateCustomer;

public class CreateCustomerCommand : IRequest<bool>
{
    public CreateCustomerCommand(AccountCreated payload)
    {
        Payload = payload;
    }

    public AccountCreated Payload { get; set; }
}