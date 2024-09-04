using System.IdentityModel.Tokens.Jwt;
using Order.Service.Models.Dtos;

namespace Order.Service.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IdentityDto GetUserInfoIdentity()
    {
        const string subClaimType = "sub";
        const string emailClaimType = "email";
        const string roleClaimType = "role";

        try
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].ToString().Replace("Bearer", "").Trim();
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            
            string userId = jwt.Claims.First(c => c.Type == subClaimType).Value;
            string userEmail = jwt.Claims.FirstOrDefault(c => c.Type == emailClaimType).Value ?? "";
            string userRole = jwt.Claims.First(c => c.Type == roleClaimType).Value;
            string expiresIn = jwt.ValidTo.ToString("yyyy-MM-dd HH:mm:ss");
            
            return new IdentityDto
            {
                Id = userId,
                Email = userEmail,
                Role = userRole,
                AccessToken = token,
                ExpiresIn = expiresIn
            };
            
        }
        catch (Exception e)
        {
            throw new UnauthorizedAccessException("You are not authorized to access this resource");
        }
    }
}