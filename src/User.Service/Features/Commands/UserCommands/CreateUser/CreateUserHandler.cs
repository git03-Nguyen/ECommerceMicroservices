using MediatR;
using User.Service.Repositories;

namespace User.Service.Features.Commands.UserCommands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(ILogger<CreateUserHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Data.Models.User
        {
            UserId = request.Payload.Id,
            UserName = request.Payload.UserName,
            Email = request.Payload.Email,
            Role = request.Payload.Role,
            FullName = request.Payload.FullName,
            PhoneNumber = request.Payload.PhoneNumber,
            Address = request.Payload.Address,
            PaymentDetails = string.Empty
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}