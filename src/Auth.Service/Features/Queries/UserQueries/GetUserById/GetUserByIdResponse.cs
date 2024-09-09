using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdResponse
{
    public UserDto Payload { get; set; }

    public GetUserByIdResponse(ApplicationUser user, string? role)
    {
        Payload = new UserDto(user, role);
    }
}