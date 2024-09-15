using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdResponse
{
    public GetUserByIdResponse(ApplicationUser user, string? role)
    {
        Payload = new UserDto(user, role);
    }

    public UserDto Payload { get; set; }
}