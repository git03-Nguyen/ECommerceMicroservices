namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserResponse
{
    public DeleteUserResponse(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }

    public bool IsDeleted { get; set; }
}