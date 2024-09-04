using Basket.Service.Models.Dtos;

namespace Basket.Service.Services.Identity;

public interface IIdentityService
{
    IdentityDto GetUserInfoIdentity();
}