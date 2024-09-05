using Contracts.Constants;
using Contracts.MassTransit.Events;
using MassTransit;
using MediatR;
using User.Service.Data.Models;
using User.Service.Features.Commands.CustomerCommands.CreateCustomer;
using User.Service.Features.Commands.SellerCommands.CreateSeller;

namespace User.Service.Consumers;

public class NewAccountCreatedConsumer : IConsumer<NewAccountCreated>
{
    private readonly IMediator _mediator;

    public NewAccountCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<NewAccountCreated> context)
    {
        var newAccountCreated = context.Message;
        if (newAccountCreated.Role == ApplicationRoleConstants.Customer)
        {
            await _mediator.Send(new CreateCustomerCommand(newAccountCreated));
        }
        else if (newAccountCreated.Role == ApplicationRoleConstants.Seller)
        {
            await _mediator.Send(new CreateSellerCommand(newAccountCreated));
        }
    }
}