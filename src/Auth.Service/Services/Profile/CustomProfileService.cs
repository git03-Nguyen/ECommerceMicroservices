using System.Security.Claims;
using Auth.Service.Data.Models;
using Contracts.Constants;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Auth.Service.Services.Profile;

public class CustomProfileService : IProfileService
{
    protected readonly UserManager<ApplicationUser> _userManager;

    public CustomProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);
        if (user == null) return;
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim("username", user.UserName),
            new Claim("email", user.Email),
            new Claim("role", roles.FirstOrDefault() ?? ApplicationRoleConstants.Customer)
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);

        context.IsActive = (user != null);
    }
}