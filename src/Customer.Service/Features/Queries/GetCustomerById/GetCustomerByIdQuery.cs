using MediatR;

namespace Customer.Service.Features.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<Models.Customer>
{
    public GetCustomerByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}