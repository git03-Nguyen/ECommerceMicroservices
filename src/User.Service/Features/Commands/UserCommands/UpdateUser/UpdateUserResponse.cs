using User.Service.Models.Dtos;

namespace User.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserResponse
{
    public UpdateUserResponse(Data.Models.User user)
    {
        Payload = new UserDto(user);
    }

    public UserDto Payload { get; set; }
}