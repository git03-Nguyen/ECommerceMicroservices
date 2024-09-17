using Auth.Service.Data.Models;
using Auth.Service.Services.Identity;
using Contracts.Constants;
using Contracts.Exceptions;
using Contracts.MassTransit.Messages.Events.Account.AccountCreated;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<SignUpHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public SignUpHandler(ILogger<SignUpHandler> logger, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, IIdentityService identityService, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _identityService = identityService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var username = request.Payload.UserName;
        var email = request.Payload.Email;
        var fullName = request.Payload.FullName;
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
            Email = email,
            FullName = fullName
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

            // Produce message to RabbitMQ: Account
            await PublishAccountCreatedEvent(newUser, role, request.Payload, cancellationToken);

            return new SignUpResponse(newUser, role);
        }
        catch (Exception e)
        {
            _logger.LogError("SignUpHandler.Handle: {0}", e.Message);
            throw;
        }
    }

    private async Task PublishAccountCreatedEvent(ApplicationUser newUser, string role, SignUpRequest request,
        CancellationToken cancellationToken)
    {
        var fullName = string.IsNullOrWhiteSpace(request.FullName) ? "N/A" : request.FullName;
        var phoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? "0000000000" : request.PhoneNumber;
        var address = string.IsNullOrWhiteSpace(request.Address) ? "N/A" : request.Address;

        var message = new
        {
            newUser.Id,
            newUser.UserName,
            newUser.Email,
            Role = role,

            FullName = fullName,
            PhoneNumber = phoneNumber,
            Address = address
        };

        await _publishEndpoint.Publish<IAccountCreated>(message, cancellationToken);
    }
}