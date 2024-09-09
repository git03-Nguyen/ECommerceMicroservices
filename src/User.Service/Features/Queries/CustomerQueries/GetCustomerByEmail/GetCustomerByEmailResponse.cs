using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailResponse
{
    public GetCustomerByEmailResponse(Customer customer)
    {
        Payload = new CustomerDto(customer);
    }

    public CustomerDto Payload { get; set; }
}