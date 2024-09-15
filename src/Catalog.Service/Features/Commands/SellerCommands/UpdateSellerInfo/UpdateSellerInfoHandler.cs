using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.UpdateSellerInfo;

public class UpdateSellerInfoHandler : IRequestHandler<UpdateSellerInfoCommand>
{
    private readonly ILogger<UpdateSellerInfoHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSellerInfoHandler(ILogger<UpdateSellerInfoHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateSellerInfoCommand request, CancellationToken cancellationToken)
    {
        var seller = await _unitOfWork.SellerRepository.GetByIdAsync(request.Payload.UserId);
        if (seller == null) throw new ResourceNotFoundException(nameof(Seller), request.Payload.UserId.ToString());
        
        if (seller.Name != request.Payload.FullName)
        {
            seller.Name = request.Payload.FullName;
            _unitOfWork.SellerRepository.Update(seller);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("UpdateSellerInfoHandler.Handle: {id} - {0}", request.Payload.UserId, request.Payload.FullName);
    }
}