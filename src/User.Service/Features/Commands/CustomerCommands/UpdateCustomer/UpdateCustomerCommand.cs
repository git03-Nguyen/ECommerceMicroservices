using MediatR;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdateCustomerResponse>
{
    public UpdateCustomerCommand(UpdateCustomerRequest payload)
    {
        Payload = payload;
    }

    public UpdateCustomerRequest Payload { get; set; }
}