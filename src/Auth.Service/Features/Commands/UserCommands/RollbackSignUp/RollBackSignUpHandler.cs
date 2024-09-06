using Auth.Service.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.RollbackSignUp;

public class RollBackSignUpHandler : IRequestHandler<RollBackSignUpCommand>
{
    private readonly ILogger<RollBackSignUpHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public RollBackSignUpHandler(UserManager<ApplicationUser> userManager, ILogger<RollBackSignUpHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task Handle(RollBackSignUpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Payload.Id.ToString());
        if (user != null)
        {
            _logger.LogInformation("RollBackSignUpHandler.Handle: {id} - {0} - {1}", user.Id, user.Email,
                user.UserName);
            await _userManager.DeleteAsync(user);
        }
    }
}