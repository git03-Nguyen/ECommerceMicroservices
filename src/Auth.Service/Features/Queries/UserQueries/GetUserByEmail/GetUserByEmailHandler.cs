using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;
using Contracts.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailHandler: IRequestHandler<GetUserByEmailQuery, GetUserByEmailResponse>
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
        if (user == null)
        {
            throw new Exception($"User with email {request.Payload.Email} not found");
        }
        var userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            FullName = user.FullName,
            Roles = await _userManager.GetRolesAsync(user)
        };
        
        return new GetUserByEmailResponse(userDto);
    }
}