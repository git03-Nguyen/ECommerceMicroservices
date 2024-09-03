using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.CustomerQueries.GetAllCustomers;

public class GetAllCustomersResponse
{
    public IEnumerable<CustomerDto> Payload { get; set; }
    
    public GetAllCustomersResponse(IEnumerable<Customer> customers)
    {
        Payload = customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            Username = c.Username,
            FullName = c.FullName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Address = c.Address,
            PaymentMethod = c.PaymentMethod
        });
    }
}