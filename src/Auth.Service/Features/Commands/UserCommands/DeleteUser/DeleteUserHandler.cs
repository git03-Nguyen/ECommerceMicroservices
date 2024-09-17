using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Exceptions;
using Contracts.MassTransit.Messages.Events.Account.AccountDeleted;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserHandler(UserManager<ApplicationUser> userManager, ILogger<DeleteUserHandler> logger,
        IIdentityService identityService, IPublishEndpoint publishEndpoint)
    {
        _userManager = userManager;
        _logger = logger;
        _identityService = identityService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user is Admin or Resource Owner
        var isAdmin = _identityService.IsUserAdmin();
        var isResourceOwner = _identityService.IsResourceOwnerById(request.UserId.ToString());
        if (!isAdmin && !isResourceOwner) throw new UnAuthorizedAccessException();

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null || user.IsDeleted)
            throw new ResourceNotFoundException(nameof(ApplicationUser), request.UserId.ToString());

        // Soft delete user
        user.Delete();
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded) throw new Exception("Failed to delete user: " + result.Errors);

        _logger.LogInformation("User with id: {UserId} has been deleted", request.UserId);

        // Publish event: IAccountDeleted
        await PublishAccountDeletedEvent(user, cancellationToken);

        return new DeleteUserResponse(true);
    }

    private async Task PublishAccountDeletedEvent(ApplicationUser user, CancellationToken cancellationToken)
    {
        var role = (await _userManager.GetRolesAsync(user)).First();
        var accountDeleted = new
        {
            AccountId = user.Id,
            Role = role
        };
        await _publishEndpoint.Publish<IAccountDeleted>(accountDeleted, cancellationToken);
    }
}