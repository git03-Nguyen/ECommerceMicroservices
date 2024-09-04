using User.Service.Models.Dtos;

namespace User.Service.Services.Identity;

public interface IIdentityService
{
    IdentityDto GetUserInfoIdentity();
}