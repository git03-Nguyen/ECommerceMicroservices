using MediatR;
using User.Service.Repositories;

namespace User.Service.Features.Commands.UserCommands.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly ILogger<DeleteUserHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUnitOfWork unitOfWork, ILogger<DeleteUserHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var deletedUser = await _unitOfWork.UserRepository.GetByIdAsync(request.AccountId);
        _unitOfWork.UserRepository.Remove(deletedUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Deleting customer with account id: {UserId}", request.AccountId);
    }
}