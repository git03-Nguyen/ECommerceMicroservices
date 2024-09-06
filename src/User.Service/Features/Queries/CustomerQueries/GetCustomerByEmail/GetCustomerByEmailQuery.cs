using MediatR;

namespace User.Service.Features.Queries.CustomerQueries.GetCustomerByEmail;

public class GetCustomerByEmailQuery : IRequest<GetCustomerByEmailResponse>
{
    public GetCustomerByEmailQuery(GetCustomerByEmailRequest payload)
    {
        Payload = payload;
    }

    public GetCustomerByEmailRequest Payload { get; set; }
}