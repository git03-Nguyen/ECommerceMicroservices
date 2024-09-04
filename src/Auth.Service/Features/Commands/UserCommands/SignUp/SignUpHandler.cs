using Auth.Service.Data.Models;
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
        var fullName = request.Payload.FullName;
        var roles = request.Payload.Roles;
        
        _logger.LogInformation("SignUpHandler.Handle: {0} - {1} - {2} - {3} - {4}", username, email, password, fullName, roles);

        var newUser = new ApplicationUser
        {
            UserName = username,
            Email = email,
            FullName = fullName
        };

        try
        {
            // Check role exists
            foreach (var role in roles)
            {
                var existingRole = await _roleManager.RoleExistsAsync(role);
                if (!existingRole) throw new Exception("Role does not exist");
            }

            // Check user exists
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null) throw new Exception("Email already exists");
            existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null) throw new Exception("Username already exists");

            // Create user
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded) throw new Exception(result.Errors.ToString());

            // Add user to roles
            foreach (var role in roles)
            {
                result = await _userManager.AddToRoleAsync(newUser, role);
                if (!result.Succeeded)
                {
                    // Delete user if adding to role failed
                    await _userManager.DeleteAsync(newUser);
                    throw new Exception(result.Errors.ToString());
                }
            }
            
        } 
        catch (Exception e)
        {
            _logger.LogError("SignUpHandler.Handle: {0}", e.Message);
            throw;
        }

        // TODO: Produce message to RabbitMQ: UserCreated
        
        return new SignUpResponse(newUser);
    }
}