using System.IdentityModel.Tokens.Jwt;
using Auth.Service.Models.Dtos;
using Contracts.Constants;

namespace Auth.Service.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSecurityToken? _jwt;
    private readonly ILogger<IdentityService> _logger;
    
    private const string subClaimType = "sub";
    private const string emailClaimType = "email";
    private const string roleClaimType = "role";

    public IdentityService(IHttpContextAccessor httpContextAccessor, ILogger<IdentityService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        try
        {
            _jwt = new JwtSecurityTokenHandler().ReadJwtToken(_httpContextAccessor.HttpContext.Request.Headers["authorization"].ToString().Replace("Bearer", "").Trim());
        }
        catch (Exception e)
        {
            _logger.LogWarning("Not able to read jwt token");
            _jwt = null;
        }
    }

    public IdentityDto GetUserInfoIdentity()
    {
        try
        {
            var jwt = _jwt ?? throw new Exception();
            
            string userId = jwt.Claims.First(c => c.Type == subClaimType).Value;
            string userEmail = jwt.Claims.First(c => c.Type == emailClaimType).Value;
            string userRole = jwt.Claims.First(c => c.Type == roleClaimType).Value;
            string expiresIn = jwt.ValidTo.ToString("yyyy-MM-dd HH:mm:ss");
            
            return new IdentityDto
            {
                Id = userId,
                Email = userEmail,
                Role = userRole,
                ExpiresIn = expiresIn
            };
        }
        catch (Exception e)
        {
            throw new UnauthorizedAccessException("Unauthorized access");
        }
        
    }

    public bool IsUserAdmin()
    {
        try
        {
            string userRole = _jwt.Claims.First(c => c.Type == roleClaimType).Value;
            return userRole == ApplicationRoleConstants.Admin;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool IsResourceOwnerById(string userId)
    {
        try
        {
            string userIdFromToken = _jwt.Claims.First(c => c.Type == subClaimType).Value;
            return userIdFromToken == userId;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public bool IsResourceOwnerByEmail(string email)
    {
        try
        {
            string userEmail = _jwt.Claims.FirstOrDefault(c => c.Type == emailClaimType).Value ?? "";
            return userEmail == email;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}