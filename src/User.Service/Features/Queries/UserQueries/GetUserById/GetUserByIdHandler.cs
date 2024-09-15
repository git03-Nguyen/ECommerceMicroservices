using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using User.Service.Repositories;

namespace User.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Check if user is admin or owner
        _identityService.EnsureIsAdminOrOwner(request.Id);

        var user = _unitOfWork.UserRepository.GetByCondition(x => x.UserId == request.Id).FirstOrDefault();
        if (user == null) throw new ResourceNotFoundException("User", request.Id.ToString());
        return new GetUserByIdResponse(user);
    }
}