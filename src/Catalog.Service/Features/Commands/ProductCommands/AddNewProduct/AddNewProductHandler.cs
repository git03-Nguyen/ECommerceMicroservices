using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Messages.Commands;
using Contracts.Services.Identity;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.AddNewProduct;

public class AddNewProductHandler : IRequestHandler<AddNewProductCommand, AddNewProductResponse>
{
    private readonly ILogger<AddNewProductHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
    private readonly ISendEndpointCustomProvider _sendEndpointCustomProvider;

    public AddNewProductHandler(IUnitOfWork unitOfWork, ILogger<AddNewProductHandler> logger, IIdentityService identityService, ISendEndpointCustomProvider sendEndpointCustomProvider)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _identityService = identityService;
        _sendEndpointCustomProvider = sendEndpointCustomProvider;
    }

    public async Task<AddNewProductResponse> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        var user = _identityService.GetUserInfoIdentity();
        
        bool isOwnImage = false;
        if (request.Payload.ImageUpload != null && request.Payload.ImageUpload.Length > 0)
        {
            // Generate a unique filename for the image and store it.
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Payload.ImageUpload.FileName)}";
            var filePath = Path.Combine("wwwroot/images", fileName);
            // Ensure the directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());
            // Save the image to the specified path.
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Payload.ImageUpload.CopyToAsync(stream, cancellationToken);
            }

            // Set the image URL for the product (adjust URL based on your server setup).
            request.Payload.ImageUrl = $"/images/{fileName}";
            isOwnImage = true;
        }
        
        // Find the seller by the user ID.
        var seller = await _unitOfWork.SellerRepository.GetByIdAsync(user.Id);
        if (seller == null)
        {
            seller = new Seller() { SellerId = Guid.Parse(user.Id), Name = user.FullName };
        }
        
        var product = new Product
        {
            Name = request.Payload.Name,
            Description = request.Payload.Description,
            ImageUrl = request.Payload.ImageUrl ?? string.Empty,
            Price = request.Payload.Price,
            CategoryId = request.Payload.CategoryId,
            Stock = request.Payload.Stock,
            SellerId = seller.SellerId,
            Seller = seller,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            IsOwnImage = isOwnImage
        };

        var success = await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (!success) throw new Exception("Failed to add new product");
        
        // Send 
        await SendCreateProductCommand(product, cancellationToken);


        return new AddNewProductResponse(product);
    }

    private async Task SendCreateProductCommand(Product product, CancellationToken cancellationToken)
    {
        var message = new
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Stock = product.Stock
        };
        await _sendEndpointCustomProvider.SendMessage<ICreateProduct>(message, cancellationToken);
    }
}