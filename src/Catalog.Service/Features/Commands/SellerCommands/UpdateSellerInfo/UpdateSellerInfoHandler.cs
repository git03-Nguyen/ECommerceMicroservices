using Catalog.Service.Repositories;
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
        var products = _unitOfWork.ProductRepository.GetByCondition(x => x.SellerAccountId == request.Payload.AccountId);
        foreach (var product in products)
        {
            if (product.SellerName != request.Payload.FullName)
                product.SellerName = request.Payload.FullName;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("UpdateSellerInfoHandler.Handle: {id} - {0}", request.Payload.AccountId, request.Payload.FullName);
    }
}