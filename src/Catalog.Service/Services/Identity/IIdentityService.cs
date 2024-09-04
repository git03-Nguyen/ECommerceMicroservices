using Catalog.Service.Models.Dtos;

namespace Catalog.Service.Services.Identity;

public interface IIdentityService
{
    IdentityDto GetUserInfoIdentity();
}