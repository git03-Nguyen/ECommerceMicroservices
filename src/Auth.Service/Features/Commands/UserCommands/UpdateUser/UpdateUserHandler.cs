using Auth.Service.Data.Models;
using Auth.Service.Exceptions;
using Auth.Service.Services.Identity;
using Contracts.Constants;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly ILogger<UpdateUserHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IPublishEndpointCustomProvider _publishEndpointCustomProvider;

    public UpdateUserHandler(ILogger<UpdateUserHandler> logger, UserManager<ApplicationUser> userManager, IIdentityService identityService, IPublishEndpointCustomProvider publishEndpointCustomProvider)
    {
        _logger = logger;
        _userManager = userManager;
        _identityService = identityService;
        _publishEndpointCustomProvider = publishEndpointCustomProvider;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Check if Admin or User is updating their own profile
        var isAdmin = _identityService.IsUserAdmin();
        var isOwner = _identityService.IsResourceOwnerById(request.Payload.Id);
        if (!isAdmin && !isOwner)
        {
            throw new UnAuthorizedAccessException();
        }
        
        // Check if user exists
        var user = await _userManager.FindByIdAsync(request.Payload.Id);
        if (user == null || user.IsDeleted)
        {
            throw new ResourceNotFoundException("userId", request.Payload.Id);
        }

        bool changed = false;
        request.Payload.UserName = request.Payload.UserName?.Trim();
        request.Payload.Email = request.Payload.Email?.Trim();
        
        // Check if email is already taken
        if (!string.IsNullOrEmpty(request.Payload.Email))
        {
            var emailExists = await _userManager.FindByEmailAsync(request.Payload.Email);
            if ((emailExists == null || emailExists.IsDeleted) && user.Email != request.Payload.Email)
            {
                user.Email = request.Payload.Email;
                changed = true;
            }
            else
            {
                throw new ResourceAlreadyExistsException("email", request.Payload.Email);
            }
        }
        
        // Check if username is already taken
        if (!string.IsNullOrEmpty(request.Payload.UserName))
        {
            var userNameExists = await _userManager.FindByNameAsync(request.Payload.UserName);
            if ((userNameExists == null || userNameExists.IsDeleted) && user.UserName != request.Payload.UserName)
            {
                user.UserName = request.Payload.UserName;
                changed = true;
            }
            else
            {
                throw new ResourceAlreadyExistsException("username", request.Payload.UserName);
            }
        }

        if (!changed)
        {
            return new UpdateUserResponse(user);
        }

        // Update user
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update user", new AggregateException(result.Errors.Select(e => new Exception(e.Description))));
        }
        
        // Publish event: AccountUpdated
        var role = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
        if (role != ApplicationRoleConstants.Admin)
        {
            await _publishEndpointCustomProvider.PublishMessage<AccountUpdated>(new AccountUpdated
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role ?? ApplicationRoleConstants.Admin
            }, cancellationToken);
        }
        
        return new UpdateUserResponse(user);
    }
}
