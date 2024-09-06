using Order.Service.Models.Dtos;

namespace Order.Service.Services.Identity;

public interface IIdentityService
{
    Guid GetUserId();
    IdentityDto GetUserInfoIdentity();
    bool IsAdmin();

    bool IsResourceOwner(Guid userId);
}