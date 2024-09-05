using Basket.Service.Models.Dtos;

namespace Basket.Service.Services.Identity;

public interface IIdentityService
{
    Guid GetUserId();
    IdentityDto GetUserInfoIdentity();
    bool IsAdmin();
    
    bool IsResourceOwner(Guid userId);
}