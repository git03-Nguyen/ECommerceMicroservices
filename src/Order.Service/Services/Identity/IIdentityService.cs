using Order.Service.Models.Dtos;

namespace Order.Service.Services.Identity;

public interface IIdentityService
{
    IdentityDto GetUserInfoIdentity();
}