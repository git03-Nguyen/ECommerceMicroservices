using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAll().ToListAsync(cancellationToken);
        return new GetAllUsersResponse(users);
    }
}