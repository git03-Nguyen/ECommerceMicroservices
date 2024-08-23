using MediatR;

namespace Customer.Service.Features.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<Models.Customer>
{
    public int Id { get; set; }

    public GetCustomerByIdQuery(int id)
    {
        Id = id;
    }
    
}