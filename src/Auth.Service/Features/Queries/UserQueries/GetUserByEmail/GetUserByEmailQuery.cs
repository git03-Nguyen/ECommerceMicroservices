using MediatR;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<GetUserByEmailResponse>
{
    public GetUserByEmailRequest Payload { get; set; }
    
    public GetUserByEmailQuery(GetUserByEmailRequest payload)
    {
        Payload = payload;
    }
    
}