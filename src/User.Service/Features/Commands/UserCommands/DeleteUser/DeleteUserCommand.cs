using MediatR;

namespace User.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public DeleteUserCommand(Guid accountId)
    {
        AccountId = accountId;
    }

    public Guid AccountId { get; set; }
}