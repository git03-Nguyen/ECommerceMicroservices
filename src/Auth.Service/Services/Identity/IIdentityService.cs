using Auth.Service.Models.Dtos;

namespace Auth.Service.Services.Identity;

public interface IIdentityService
{
    IdentityDto GetUserInfoIdentity();
}