using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerResponse
{
    public CustomerDto Payload { get; set; }
    
    public UpdateCustomerResponse(Customer customer)
    {
        Payload = new CustomerDto
        {
            Id = customer.Id,
            Email = customer.Email,
            Username = customer.Username,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            PaymentMethod = customer.PaymentMethod
        };
    }
}