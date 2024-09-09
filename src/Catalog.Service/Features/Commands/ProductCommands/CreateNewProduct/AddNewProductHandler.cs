using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Services.Identity;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class AddNewProductHandler : IRequestHandler<AddNewProductCommand, AddNewProductResponse>
{
    private readonly ILogger<AddNewProductHandler> _logger;
    private readonly ICatalogUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public AddNewProductHandler(ICatalogUnitOfWork unitOfWork, ILogger<AddNewProductHandler> logger, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<AddNewProductResponse> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserId();
        
        var product = new Product
        {
            Name = request.Payload.Name,
            Description = request.Payload.Description,
            ImageUrl = request.Payload.ImageUrl,
            Price = request.Payload.Price,
            CategoryId = request.Payload.CategoryId,
            Stock = request.Payload.AvailableStock,
            SellerAccountId = userId,
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