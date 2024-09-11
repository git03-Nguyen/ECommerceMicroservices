using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdResponse
{
    public UserDto Payload { get; set; }
    
    public GetUserByIdResponse(Data.Models.User user)
    {
        Payload = new UserDto(user);
    }
}