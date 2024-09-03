using MediatR;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailQuery : IRequest<GetCustomerByEmailResponse>
{
    public GetCustomerByEmailRequest Payload { get; set; }
    
    public GetCustomerByEmailQuery(GetCustomerByEmailRequest payload)
    {
        Payload = payload;
    }
    
}