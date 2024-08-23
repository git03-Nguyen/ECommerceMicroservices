using MediatR;

namespace Customer.Service.Features.Queries.GetAllCustomers;

public class GetAllCustomersQuery : IRequest<IEnumerable<Models.Customer>>
{
}