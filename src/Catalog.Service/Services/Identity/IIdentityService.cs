using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Services.Identity;

public interface IIdentityService
{
    Guid GetUserId();
    IdentityDto GetUserInfoIdentity();
    bool IsAdmin();

    bool IsResourceOwner(Guid userId);
}