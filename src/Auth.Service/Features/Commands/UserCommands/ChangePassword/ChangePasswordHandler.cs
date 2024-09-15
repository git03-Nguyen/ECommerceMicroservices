using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly ILogger<ChangePasswordHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public ChangePasswordHandler(ILogger<ChangePasswordHandler> logger, UserManager<ApplicationUser> userManager, IIdentityService identityService)
    {
        _logger = logger;
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserInfoIdentity().Id;
        var user = await _userManager.FindByIdAsync(userId);
        var result = await _userManager.ChangePasswordAsync(user, request.Payload.Password, request.Payload.NewPassword);

        if (!result.Succeeded)
        {
            _logger.LogError("Failed to change password for user {UserId}", userId);
            throw new Exception("Failed to change password");
        }

        _logger.LogInformation("Password changed for user {UserId}", userId);
    }
}