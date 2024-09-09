using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerResponse
{
    public UpdateCustomerResponse(Customer customer)
    {
        Payload = new CustomerDto(customer);
    }

    public CustomerDto Payload { get; set; }
}