using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Constants;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Messages.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly IPublishEndpointCustomProvider _publishEndpointCustomProvider;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserHandler(UserManager<ApplicationUser> userManager, ILogger<DeleteUserHandler> logger,
        IIdentityService identityService, IPublishEndpointCustomProvider publishEndpointCustomProvider)
    {
        _userManager = userManager;
        _logger = logger;
        _identityService = identityService;
        _publishEndpointCustomProvider = publishEndpointCustomProvider;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user is Admin or Resource Owner
        var isAdmin = _identityService.IsUserAdmin();
        var isResourceOwner = _identityService.IsResourceOwnerByEmail(request.Payload.Email);
        if (!isAdmin && !isResourceOwner) throw new UnAuthorizedAccessException();

        var user = await _userManager.FindByEmailAsync(request.Payload.Email);
        if (user == null || user.IsDeleted) throw new ResourceNotFoundException("email", request.Payload.Email);

        // Soft delete user
        user.Delete();
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) throw new Exception("Failed to delete user: " + result.Errors);

        _logger.LogInformation("User with email {0} deleted", request.Payload.Email);

        var roles = await _userManager.GetRolesAsync(user);
        if (roles[0] != ApplicationRoleConstants.Admin)
        {
            // Publish event: AccountDeleted
            var accountDeleted = new AccountDeleted
            {
                AccountId = user.Id,
                Role = roles[0]
            };
            await _publishEndpointCustomProvider.PublishMessage(accountDeleted, cancellationToken);
        }

        return new DeleteUserResponse();
    }
}