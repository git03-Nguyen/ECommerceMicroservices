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

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.subClaimType);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        return Guid.Parse(userId);
    }

    public IdentityDto GetUserInfoIdentity()
    {
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.subClaimType);
        var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.userNameClaimType);
        var email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.emailClaimType);
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.roleClaimType);

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role))
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        return new IdentityDto
        {
            Id = userId,
            UserName = userName,
            Email = email,
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
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.roleClaimType);
        if (role != ApplicationRoleConstants.Admin)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsSeller()
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.roleClaimType);
        if (role != ApplicationRoleConstants.Seller)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsAdminOrOwner(Guid ownerId)
    {
        var userId = GetUserId();
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.roleClaimType);
        if (role != ApplicationRoleConstants.Admin && userId != ownerId)
            throw new ForbiddenAccessException();
    }

    public void EnsureIsCustomer()
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimConstants.roleClaimType);
        if (role != ApplicationRoleConstants.Customer)
            throw new ForbiddenAccessException();
    }
}