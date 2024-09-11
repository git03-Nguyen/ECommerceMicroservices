namespace Auth.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserResponse
{
    public bool IsDeleted { get; set; }
    
    public DeleteUserResponse(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }
}