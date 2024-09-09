using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Service.Repositories;

namespace User.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerHandler : IRequestHandler<UpdateSellerCommand, UpdateSellerResponse>
{
    private readonly ILogger<UpdateSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public UpdateSellerHandler(ILogger<UpdateSellerHandler> logger, IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<UpdateSellerResponse> Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
    {
        // Check owner or admin
        _identityService.EnsureIsAdminOrOwner(request.Payload.AccountId);
        
        var seller = await _unitOfWork.SellerRepository.GetByCondition(x => x.AccountId == request.Payload.AccountId)
            .FirstOrDefaultAsync(cancellationToken);
        if (seller == null) throw new ResourceNotFoundException("AccountId", request.Payload.AccountId.ToString());

        seller.Account.Email = string.IsNullOrWhiteSpace(request.Payload.Email)
            ? seller.Account.Email
            : request.Payload.Email;
        seller.Account.UserName = string.IsNullOrWhiteSpace(request.Payload.UserName)
            ? seller.Account.UserName
            : request.Payload.UserName;
        seller.FullName = string.IsNullOrWhiteSpace(request.Payload.FullName)
            ? seller.FullName
            : request.Payload.FullName;
        seller.PhoneNumber = string.IsNullOrWhiteSpace(request.Payload.PhoneNumber)
            ? seller.PhoneNumber
            : request.Payload.PhoneNumber;
        seller.Address = string.IsNullOrWhiteSpace(request.Payload.Address) ? seller.Address : request.Payload.Address;
        seller.PaymentMethod = string.IsNullOrWhiteSpace(request.Payload.PaymentMethod)
            ? seller.PaymentMethod
            : request.Payload.PaymentMethod;

        _unitOfWork.SellerRepository.Update(seller);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("UpdateSellerHandler.Handle: {id} - {0} - {1} - {2} - {3} - {4}", seller.AccountId,
            seller.Account.Email, seller.Account.UserName, seller.FullName, seller.PhoneNumber, seller.Address);
        
        // TODO: Produce a message: SellerUpdatedEvent

        return new UpdateSellerResponse(seller);
    }
}