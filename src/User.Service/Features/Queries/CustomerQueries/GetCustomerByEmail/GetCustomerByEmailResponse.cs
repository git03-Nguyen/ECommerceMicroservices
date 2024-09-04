using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailResponse
{
    public CustomerDto Payload { get; set; }
    
    public GetCustomerByEmailResponse(Customer customer)
    {
        Payload = new CustomerDto
        {
            Id = customer.Id,
            Username = customer.Username,
            FullName = customer.FullName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            PaymentMethod = customer.PaymentMethod
        };
    }
}