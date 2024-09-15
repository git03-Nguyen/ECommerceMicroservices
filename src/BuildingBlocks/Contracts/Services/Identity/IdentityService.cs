using System.Security.Claims;
using Contracts.Constants;
using Contracts.Exceptions;
using Contracts.Models;
using Microsoft.AspNetCore.Http;

namespace Contracts.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.SubClaimType);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        return Guid.Parse(userId);
    }

    public IdentityDto GetUserInfoIdentity()
    {
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.SubClaimType);
        var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.UserNameClaimType);
        var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.EmailClaimType);
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.RoleClaimType);
        var fullName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.FullNameClaimType);

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        return new IdentityDto
        {
            Id = userId,
            UserName = userName,
            Email = email,
            FullName = fullName,
            Role = role
        };
    }

    public void EnsureIsResourceOwner(Guid ownerId)
    {
        var userId = GetUserId();
        if (userId != ownerId)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsAdmin()
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.RoleClaimType);
        if (role != ApplicationRoleConstants.Admin)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsSeller()
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.RoleClaimType);
        if (role != ApplicationRoleConstants.Seller)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsAdminOrOwner(Guid ownerId)
    {
        var userId = GetUserId();
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.RoleClaimType);
        if (role != ApplicationRoleConstants.Admin && userId != ownerId)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsCustomer()
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.RoleClaimType);
        if (role != ApplicationRoleConstants.Customer)
            throw new ForbiddenAccessException();
    }
}