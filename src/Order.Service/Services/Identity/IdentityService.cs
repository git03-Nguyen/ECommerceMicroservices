using System.Security.Claims;
using Contracts.Constants;
using Order.Service.Models.Dtos;

namespace Order.Service.Services.Identity;

public class IdentityService : IIdentityService
{
    private const string subClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    private const string userNameClaimType = "username";
    private const string emailClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    private const string roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(subClaimType);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        return Guid.Parse(userId);
    }

    public IdentityDto GetUserInfoIdentity()
    {
        if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException("You are not authorized to access this resource");

        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(subClaimType);
        var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(userNameClaimType);
        var email = _httpContextAccessor.HttpContext.User.FindFirstValue(emailClaimType);
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(roleClaimType);

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

    public bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext.User.HasClaim(roleClaimType, ApplicationRoleConstants.Admin);
    }

    public bool IsResourceOwner(Guid userId)
    {
        var sub = _httpContextAccessor.HttpContext.User.FindFirstValue(subClaimType);
        return sub == userId.ToString();
    }
}