using User.Service.Models.Dtos;

namespace User.Service.Services.Identity;

public interface IIdentityService
{
    Guid GetUserId();
    IdentityDto GetUserInfoIdentity();
    bool IsAdmin();
    
    bool IsResourceOwner(Guid userId);
}