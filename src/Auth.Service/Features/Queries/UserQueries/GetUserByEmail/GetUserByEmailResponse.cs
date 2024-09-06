using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailResponse
{
    public GetUserByEmailResponse(UserDto user)
    {
        Payload = user;
    }

    public UserDto Payload { get; set; }
}