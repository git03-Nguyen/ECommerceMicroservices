using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailResponse
{
    public GetCustomerByEmailResponse(Customer customer)
    {
        Payload = new CustomerDto
        {
            Id = customer.CustomerId,
            AccountId = customer.AccountId,
            Username = customer.Account.UserName,
            Email = customer.Account.Email,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            PaymentMethod = customer.PaymentMethod
        };
    }

    public CustomerDto Payload { get; set; }
}