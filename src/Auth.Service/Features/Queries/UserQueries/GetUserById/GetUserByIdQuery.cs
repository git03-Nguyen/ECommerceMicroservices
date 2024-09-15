using MediatR;

namespace Auth.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}