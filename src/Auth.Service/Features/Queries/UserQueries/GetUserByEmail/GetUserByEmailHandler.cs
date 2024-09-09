using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;
using Contracts.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>
{
    private readonly ILogger<GetUserByEmailHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserByEmailHandler(ILogger<GetUserByEmailHandler> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<GetUserByEmailResponse> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Payload.Email);
        if (user == null || user.IsDeleted) throw new ResourceNotFoundException("email", request.Payload.Email);
        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        var userDto = new UserDto(user, role);

        return new GetUserByEmailResponse(userDto);
    }
}