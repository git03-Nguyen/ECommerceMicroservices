namespace Auth.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserRequest
{
    public required string Id { get; set; }
    public string? UserName { get; set; } = null;
    public string? Email { get; set; } = null;
}