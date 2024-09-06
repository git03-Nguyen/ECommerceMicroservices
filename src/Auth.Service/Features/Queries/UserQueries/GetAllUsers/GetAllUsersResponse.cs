using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersResponse
{
    public GetAllUsersResponse(IEnumerable<UserDto> users)
    {
        Payload = users;
    }

    public IEnumerable<UserDto> Payload { get; set; }
}