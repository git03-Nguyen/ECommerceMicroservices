using Auth.Service.Models.Dtos;

namespace Auth.Service.Services.Identity;

public interface IIdentityService
{
    IdentityDto GetUserInfoIdentity();
    
    bool IsUserAdmin();
    
    bool IsResourceOwnerById(string userId);
    bool IsResourceOwnerByEmail(string email);
}