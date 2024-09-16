using Catalog.Service.Repositories;
using MassTransit;
using MediatR;

namespace Catalog.Service.Features.Commands.SellerCommands.DeleteSeller;

public class DeleteSellerHandler : IRequestHandler<DeleteSellerCommand>
{
    private readonly ILogger<DeleteSellerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSellerHandler(ILogger<DeleteSellerHandler> logger, IUnitOfWork unitOfWork  )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _unitOfWork.SellerRepository.GetByIdAsync(request.Payload.AccountId);
        if (seller == null)
        {
            _logger.LogWarning("Seller with id {SellerId} was not found", request.Payload.AccountId);
            return;
        }
        var products = _unitOfWork.ProductRepository.GetByCondition(x => x.SellerId == request.Payload.AccountId);
        _unitOfWork.ProductRepository.RemoveRange(products);
        _unitOfWork.SellerRepository.Remove(seller);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Products of seller with id {SellerId} deleted", request.Payload.AccountId);
        _logger.LogInformation("Seller with id {SellerId} deleted", request.Payload.AccountId);
    }
}