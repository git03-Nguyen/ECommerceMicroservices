using Auth.Service.Data.Models;
using Contracts.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.UpdateAccountInfo;

public class UpdateAccountInfoHandler : IRequestHandler<UpdateAccountInfoCommand>
{
    private readonly ILogger<UpdateAccountInfoHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateAccountInfoHandler(ILogger<UpdateAccountInfoHandler> logger, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Payload.UserId.ToString());
        if (user == null) throw new ResourceNotFoundException(nameof(ApplicationUser), request.Payload.UserId.ToString());

        user.Email = string.IsNullOrWhiteSpace(request.Payload.Email) ? user.Email : request.Payload.Email;
        user.UserName = string.IsNullOrWhiteSpace(request.Payload.UserName) ? user.UserName : request.Payload.UserName;
        user.PhoneNumber = string.IsNullOrWhiteSpace(request.Payload.PhoneNumber) ? user.PhoneNumber : request.Payload.PhoneNumber;
        user.FullName = string.IsNullOrWhiteSpace(request.Payload.FullName) ? user.FullName : request.Payload.FullName;

        await _userManager.UpdateAsync(user);
        _logger.LogInformation("UpdateAccountInfoHandler.Handle: {id} - {0} - {1} - {2}", user.Id, user.Email, user.UserName, user.PhoneNumber);
        
    }
}