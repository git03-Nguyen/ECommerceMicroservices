using Contracts.Constants;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using User.Service.Features.Commands.CustomerCommands.UpdateCustomer;
using User.Service.Features.Commands.SellerCommands.UpdateSeller;

namespace User.Service.Consumers;

public class AccountUpdatedConsumer : IConsumer<AccountUpdated>
{
    private readonly IMediator _mediator;

    public AccountUpdatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AccountUpdated> context)
    {
        var accountUpdated = context.Message;
        if (accountUpdated.Role == ApplicationRoleConstants.Customer)
        {
            var request = new UpdateCustomerRequest
            {
                AccountId = accountUpdated.Id,
                Email = accountUpdated.Email,
                UserName = accountUpdated.UserName
            };
            await _mediator.Send(new UpdateCustomerCommand(request));
        }
        else if (accountUpdated.Role == ApplicationRoleConstants.Seller)
        {
            var request = new UpdateSellerRequest
            {
                AccountId = accountUpdated.Id,
                Email = accountUpdated.Email,
                UserName = accountUpdated.UserName
            };
            await _mediator.Send(new UpdateSellerCommand(request));
        }
    }
}