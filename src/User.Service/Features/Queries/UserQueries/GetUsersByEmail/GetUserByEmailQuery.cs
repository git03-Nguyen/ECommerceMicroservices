using MediatR;

namespace User.Service.Features.Queries.UserQueries.GetUsersByEmail;

public class GetUserByEmailQuery : IRequest<GetUserByEmailResponse>
{
    public GetUserByEmailQuery(GetUserByEmailRequest payload)
    {
        Payload = payload;
    }

    public GetUserByEmailRequest Payload { get; set; }
}