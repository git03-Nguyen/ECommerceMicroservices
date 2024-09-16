using Basket.Service.Repositories;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.UpdateSeller;

public class UpdateSellerHandler : IRequestHandler<UpdateSellerCommand>
{
    private readonly ILogger<UpdateSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public UpdateSellerHandler(ILogger<UpdateSellerHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _unitOfWork.SellerRepository.GetByIdAsync(request.Payload.UserId);
        if (seller == null)
        {
            _logger.LogWarning("Seller with id {SellerId} was not found", request.Payload.UserId);
            return;
        }
        seller.Name = request.Payload.FullName;
        _unitOfWork.SellerRepository.Update(seller);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Seller with id {SellerId} updated", request.Payload.UserId);
    }
}