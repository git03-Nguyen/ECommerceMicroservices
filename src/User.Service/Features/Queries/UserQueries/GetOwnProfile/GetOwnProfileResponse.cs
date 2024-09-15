using User.Service.Models.Dtos;

namespace User.Service.Features.Queries.UserQueries.GetOwnProfile;

public class GetOwnProfileResponse
{
    public GetOwnProfileResponse(Data.Models.User user)
    {
        Payload = new UserDto(user);
    }

    public UserDto Payload { get; set; }
}