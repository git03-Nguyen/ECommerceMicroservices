using MediatR;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdateCustomerResponse>
{
    public UpdateCustomerRequest Payload { get; set; }
    
    public UpdateCustomerCommand(UpdateCustomerRequest payload)
    {
        Payload = payload;
    }
    
}