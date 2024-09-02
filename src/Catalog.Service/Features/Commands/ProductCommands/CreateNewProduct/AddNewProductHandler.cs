using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class AddNewProductHandler : IRequestHandler<AddNewProductCommand, AddNewProductResponse>
{
    private readonly ICatalogUnitOfWork _unitOfWork;

    public AddNewProductHandler(ICatalogUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddNewProductResponse> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Payload.Name,
            Description = request.Payload.Description,
            ImageUrl = request.Payload.ImageUrl,
            Price = request.Payload.Price,
            CategoryId = request.Payload.CategoryId,
            Stock = request.Payload.AvailableStock,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        // TODO: any cancelation token handling here?
        var success = await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (!success) throw new Exception("Failed to add new product");

        return new AddNewProductResponse(product);
    }
}