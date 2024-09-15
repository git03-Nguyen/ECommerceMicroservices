using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.UserQueries.GetUserById;

public class GetUserByIdResponse
{
    public GetUserByIdResponse(Data.Models.User user)
    {
        Payload = new UserDto(user);
    }

    public UserDto Payload { get; set; }
}