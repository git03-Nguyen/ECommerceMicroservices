using Auth.Service.Data.Models;
using Auth.Service.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Features.Commands.UserCommands.SignUp;

public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpResponse>
{
    private readonly ILogger<SignUpHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SignUpHandler(ILogger<SignUpHandler> logger, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        
        var username = request.Payload.UserName;
        var email = request.Payload.Email;
        var password = request.Payload.Password;
        var roles = request.Payload.Roles;
        
        _logger.LogInformation("SignUpHandler.Handle: {0} - {1} - {2} - {3} - {4}", username, email, password, roles);

        var newUser = new ApplicationUser
        {
            UserName = username,
            Email = email
        };

        try
        {
            // Check role exists
            foreach (var role in roles)
            {
                var existingRole = await _roleManager.RoleExistsAsync(role);
                if (!existingRole) throw new ResourceNotFoundException("Role", role);
            }

            // Check user exists
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null && !existingUser.IsDeleted) throw new ResourceAlreadyExistsException("User", email);
            existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null && !existingUser.IsDeleted) throw new ResourceAlreadyExistsException("User", username);

            // Create user
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded) throw new Exception("Failed to create user: " + result.Errors);

            // Add user to roles
            foreach (var role in roles)
            {
                result = await _userManager.AddToRoleAsync(newUser, role);
                if (!result.Succeeded)
                {
                    // Delete user if adding to role failed
                    await _userManager.DeleteAsync(newUser);
                    throw new Exception("Failed to add user to role: " + result.Errors);
                }
            }
            
        } 
        catch (Exception e)
        {
            _logger.LogError("SignUpHandler.Handle: {0}", e.Message);
            throw;
        }
        
        // TODO: Produce message to RabbitMQ: UserCreated
        
        return new SignUpResponse(newUser, roles);
    }
}