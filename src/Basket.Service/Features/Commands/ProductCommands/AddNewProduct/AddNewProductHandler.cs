using Basket.Service.Data.Models;
using Basket.Service.Repositories;
using MediatR;

namespace Basket.Service.Features.Commands.ProductCommands.AddNewProduct;

public class AddNewProductHandler : IRequestHandler<AddNewProductCommand>
{
    private readonly ILogger<AddNewProductHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public AddNewProductHandler(ILogger<AddNewProductHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        // Add new product to the basket
        var product = new Product()
        {
            ProductId = request.Payload.ProductId,
            ProductName = request.Payload.Name,
            UnitPrice = request.Payload.Price,
            Stock = request.Payload.Stock,
            ImageUrl = request.Payload.ImageUrl,
            SellerId = request.Payload.SellerId
        };
        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Product {ProductName} added to the basket", request.Payload.Name);
    }
}