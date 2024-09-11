using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetOwnProfileQuery;

public class GetOwnProfileResponse
{
    public UserDto Payload { get; set; }
    
    public GetOwnProfileResponse(ApplicationUser user, string? role)
    {
        Payload = new UserDto(user, role);
    }
}