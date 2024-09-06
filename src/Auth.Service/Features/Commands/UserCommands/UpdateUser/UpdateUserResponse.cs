using Auth.Service.Data.Models;

namespace Auth.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserResponse
{
    public UpdateUserResponse(ApplicationUser user)
    {
        Id = user.Id.ToString();
        UserName = user.UserName;
        Email = user.Email;
    }

    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}