using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Constants;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.PublishEndpoint;
using Contracts.MassTransit.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly ILogger<SignUpHandler> _logger;
    private readonly IPublishEndpointCustomProvider _publishEndpointCustomProvider;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IIdentityService _identityService;

    public SignUpHandler(ILogger<SignUpHandler> logger, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, IPublishEndpointCustomProvider publishEndpointCustomProvider, IIdentityService identityService)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _publishEndpointCustomProvider = publishEndpointCustomProvider;
        _identityService = identityService;
    }

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var username = request.Payload.UserName;
        var email = request.Payload.Email;
        var password = request.Payload.Password;
        var role = request.Payload.Role;

        if (role == ApplicationRoleConstants.Admin)
        {
            var isUserAdmin = _identityService.IsUserAdmin();
            if (!isUserAdmin) throw new UnAuthorizedAccessException();
        }

        _logger.LogInformation("SignUpHandler.Handle: {0} - {1} - {2} - {3}", username, email, password, role);

        var newUser = new ApplicationUser
        {
            UserName = username,
            Email = email
        };

        try
        {
            // Check role exists
            var existingRole = await _roleManager.RoleExistsAsync(role);
            if (!existingRole) throw new ResourceNotFoundException("Role", role);

            // Check user exists
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null && !existingUser.IsDeleted)
                throw new ResourceAlreadyExistsException("User", email);
            existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null && !existingUser.IsDeleted)
                throw new ResourceAlreadyExistsException("User", username);

            // Create user
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded) throw new Exception("Failed to create user: " + result.Errors);

            // Add user to roles
            result = await _userManager.AddToRoleAsync(newUser, role);
            if (!result.Succeeded)
            {
                // Delete user if adding to role failed
                await _userManager.DeleteAsync(newUser);
                throw new Exception("Failed to add user to role: " + result.Errors);
            }

            // Produce message to RabbitMQ: AccountCreated
            await PublishAccountCreatedEvent(newUser, role, cancellationToken);

            return new SignUpResponse(newUser, role);
        }
        catch (Exception e)
        {
            _logger.LogError("SignUpHandler.Handle: {0}", e.Message);
            throw;
        }
    }
    
    private async Task PublishAccountCreatedEvent(ApplicationUser newUser, string role, CancellationToken cancellationToken)
    {
        if (role == ApplicationRoleConstants.Admin) return;
        
        var message = new AccountCreated
        {
            Id = newUser.Id,
            UserName = newUser.UserName,
            Email = newUser.Email,
            Role = role
        };
        // string routingKey = (role == ApplicationRoleConstants.Customer) ? "customer.created" : "seller.created";
        await _publishEndpointCustomProvider.PublishMessage(message, cancellationToken);
    }
}