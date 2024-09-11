using MediatR;

namespace User.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdQuery : IRequest<GetUserByIdResponse>
{
    public Guid Id { get; set; }
    
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
    
}