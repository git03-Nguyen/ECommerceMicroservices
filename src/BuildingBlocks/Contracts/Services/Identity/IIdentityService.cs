using Contracts.Models;

namespace Contracts.Services.Identity;

public interface IIdentityService
{
    Guid GetUserId();
    IdentityDto GetUserInfoIdentity();
    void EnsureIsResourceOwner(Guid ownerId);
    void EnsureIsAdmin();
    void EnsureIsSeller();
    void EnsureIsAdminOrOwner(Guid ownerId);
    void EnsureIsCustomer();
}