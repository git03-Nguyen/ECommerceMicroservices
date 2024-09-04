using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly ILogger<UpdateUserHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public UpdateUserHandler(ILogger<UpdateUserHandler> logger, UserManager<ApplicationUser> userManager, IIdentityService identityService)
    {
        _logger = logger;
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Check if Admin or User is updating their own profile
        var isAdmin = _identityService.IsUserAdmin();
        var isOwner = _identityService.IsResourceOwnerById(request.Payload.Id);
        if (!isAdmin && !isOwner)
        {
            throw new UnauthorizedAccessException("Forbidden");
        }
        
        // Check if user exists
        var user = await _userManager.FindByIdAsync(request.Payload.Id);
        if (user == null || user.IsDeleted)
        {
            throw new UserNotFoundException($"User with id {request.Payload.Id} not found", request.Payload.Id);
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

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception("Failed to update user", new AggregateException(result.Errors.Select(e => new Exception(e.Description))));
        }

        return new UpdateUserResponse(user);
    }
}

public class ResourceAlreadyExistsException : Exception
{
    public string Key { get; set; }
    public string Value { get; set; }
    
    public ResourceAlreadyExistsException(string key, string value) : base($"Resource with {key}: {value} already exists")
    {
        Key = key;
        Value = value;
    }
}

public class UserNotFoundException : Exception
{
    public string Id { get; set; }
    public UserNotFoundException(string message, string id) : base(message)
    {
        Id = id;
    }
}