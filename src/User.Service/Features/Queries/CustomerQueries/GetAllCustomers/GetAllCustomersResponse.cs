using User.Service.Data.Models;
using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.CustomerQueries.GetAllCustomers;

public class GetAllCustomersResponse
{
    public GetAllCustomersResponse(IEnumerable<Customer> customers)
    {
        Payload = customers.Select(c => new CustomerDto(c));
    }

    public IEnumerable<CustomerDto> Payload { get; set; }
}