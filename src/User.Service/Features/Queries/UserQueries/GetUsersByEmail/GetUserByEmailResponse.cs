using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.UserQueries.GetUsersByEmail;

public class GetUserByEmailResponse
{
    public GetUserByEmailResponse(Data.Models.User user)
    {
        Payload = new UserDto(user);
    }

    public UserDto Payload { get; set; }
}