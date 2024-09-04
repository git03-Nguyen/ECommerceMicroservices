using Auth.Service.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<DeleteUserHandler> _logger;

    public DeleteUserHandler(UserManager<ApplicationUser> userManager, ILogger<DeleteUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Payload.Email);
        if (user == null)
        {
            throw new Exception($"User with email {request.Payload.Email} not found");
        }
        
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.ToString());
        }
        
        _logger.LogInformation("User with email {0} deleted", request.Payload.Email);
        // TODO: Produce message to delete user from other services        
        
        return new DeleteUserResponse();
    }
}