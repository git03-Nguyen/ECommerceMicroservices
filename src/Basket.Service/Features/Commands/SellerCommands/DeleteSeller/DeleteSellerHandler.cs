using Basket.Service.Consumers;
using Basket.Service.Repositories;
using MediatR;

namespace Basket.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerHandler : IRequestHandler<DeleteSellerCommand>
{
    private readonly ILogger<DeleteSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSellerHandler(ILogger<DeleteSellerHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _unitOfWork.SellerRepository.GetByIdAsync(request.Payload.AccountId);
        if (seller == null)
        {
            _logger.LogWarning("Seller not found for seller id: {Id}", request.Payload.AccountId);
            return;
        }

        _unitOfWork.SellerRepository.Remove(seller);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Seller deleted. SellerId: {SellerId}", seller.SellerId);
    }
}