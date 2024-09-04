using Auth.Service.Data.Models;
using Auth.Service.Models.Dtos;

namespace Auth.Service.Features.Queries.UserQueries.GetUserByEmail;

public class GetUserByEmailResponse
{
    public UserDto Payload { get; set; }

    public GetUserByEmailResponse(UserDto user)
    {
        Payload = user;
    }
}