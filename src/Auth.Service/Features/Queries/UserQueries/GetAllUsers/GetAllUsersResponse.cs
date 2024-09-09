using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersResponse
{
    public GetAllUsersResponse(IEnumerable<UserDto> usersDto)
    {
        Payload = usersDto;
    }

    public IEnumerable<UserDto> Payload { get; set; }
}