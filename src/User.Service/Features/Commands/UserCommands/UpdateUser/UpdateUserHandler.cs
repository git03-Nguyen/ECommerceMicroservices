using Contracts.Exceptions;
using Contracts.MassTransit.Messages.Events;
using Contracts.MassTransit.Messages.Events.Account.AccountUpdated;
using Contracts.Services.Identity;
using MassTransit;
using MediatR;
using User.Service.Repositories;

namespace User.Service.Features.Commands.UserCommands.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<UpdateUserHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(ILogger<UpdateUserHandler> logger, IUnitOfWork unitOfWork,
        IIdentityService identityService, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Check owner or admin
        _identityService.EnsureIsAdminOrOwner(request.Payload.UserId);

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Payload.UserId);
        if (user == null) throw new ResourceNotFoundException("UserId", request.Payload.UserId.ToString());

        user.Email = request.Payload.Email;
        user.UserName = request.Payload.UserName;
        user.FullName = request.Payload.FullName;
        user.PhoneNumber = request.Payload.PhoneNumber;
        user.Address = request.Payload.Address;
        user.PaymentDetails = request.Payload.PaymentDetails;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("UpdateSellerHandler.Handle: {0} - {1} - {2} - {3} - {4} - {5} - {6} - {7}",
            user.UserId, user.Email, user.UserName, user.FullName, user.PhoneNumber, user.Address, user.PaymentDetails,
            user.Role);

        // Return IUserUpdated
        await PublishUserInfoUpdatedEvent(user, cancellationToken);

        return new UpdateUserResponse(user);
    }

    private async Task PublishUserInfoUpdatedEvent(Data.Models.User user, CancellationToken cancellationToken)
    {
        var userInfoUpdated = new
        {
            user.UserId,
            user.Email,
            user.UserName,
            user.FullName,
            user.PhoneNumber,
            user.Address,
            PaymentMethod = user.PaymentDetails,
            user.Role
        };

        await _publishEndpoint.Publish<IUserUpdated>(userInfoUpdated, cancellationToken);
    }
}