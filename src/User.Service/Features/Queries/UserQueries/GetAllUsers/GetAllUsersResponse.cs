using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersResponse
{
    public GetAllUsersResponse(IEnumerable<Data.Models.User> users)
    {
        Payload = users.Select(u => new UserDto(u));
    }

    public IEnumerable<UserDto> Payload { get; set; }
}