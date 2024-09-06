using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Commands.CustomerCommands.UpdateCustomer;

public class UpdateCustomerResponse
{
    public UpdateCustomerResponse(Customer customer)
    {
        Payload = new CustomerDto
        {
            Id = customer.CustomerId,
            AccountId = customer.AccountId,
            Email = customer.Account.Email,
            Username = customer.Account.UserName,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            PaymentMethod = customer.PaymentMethod
        };
    }

    public CustomerDto Payload { get; set; }
}