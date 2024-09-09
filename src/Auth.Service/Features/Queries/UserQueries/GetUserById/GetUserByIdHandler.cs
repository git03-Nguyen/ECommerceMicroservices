using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    private readonly IIdentityService _identityService;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserByIdHandler(IIdentityService identityService, UserManager<ApplicationUser> userManager)
    {
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Check if admin or owner
        var isOwnerOrAdmin = _identityService.IsUserAdmin() || _identityService.IsResourceOwnerById(request.Id.ToString());
        if (!isOwnerOrAdmin)
        {
            throw new UnAuthorizedAccessException();
        }
        
        // Check if user exists
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
        {
            throw new ResourceNotFoundException(nameof(ApplicationUser), request.Id.ToString());
        }

        var role = (await _userManager.GetRolesAsync(user)).First();
        
        return new GetUserByIdResponse(user, role);
    }
}