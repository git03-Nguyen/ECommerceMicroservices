using MediatR;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailQuery : IRequest<GetUserByEmailResponse>
{
    public GetUserByEmailQuery(GetUserByEmailRequest payload)
    {
        Payload = payload;
    }

    public GetUserByEmailRequest Payload { get; set; }
}