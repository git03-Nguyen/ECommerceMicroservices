using Basket.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Basket.Service.Features.Commands.ProductCommands.UpdateProductsStock;

public class UpdateProductsStockHandler : IRequestHandler<UpdateProductsStockCommand>
{
    private readonly ILogger<UpdateProductsStockHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductsStockHandler(ILogger<UpdateProductsStockHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProductsStockCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.Payload.OrderItems.Select(x => x.ProductId).ToList();
        var products = await _unitOfWork.ProductRepository.GetByCondition(x => productIds.Contains(x.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var product in products)
        {
            var orderItem = request.Payload.OrderItems.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (orderItem != null)
            {
                product.Stock -= orderItem.Quantity;
                if (product.Stock < 0) product.Stock = 0;
                _unitOfWork.ProductRepository.Update(product);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Product stock updated after order created. Payload: {Payload}",
            string.Join(",", productIds));
    }
}