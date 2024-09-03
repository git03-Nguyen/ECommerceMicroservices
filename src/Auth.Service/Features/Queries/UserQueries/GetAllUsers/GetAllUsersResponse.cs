using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetAllUsers;

public class GetAllUsersResponse
{
    public IEnumerable<UserDto> Payload { get; set; }
    
    public GetAllUsersResponse(IEnumerable<UserDto> users)
    {
        Payload = users;
    }
    
}