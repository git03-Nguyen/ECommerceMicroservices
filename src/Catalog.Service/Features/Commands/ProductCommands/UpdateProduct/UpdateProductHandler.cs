using System.ComponentModel;
using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.MassTransit.Messages.Commands;
using Contracts.Services.Identity;
using MassTransit;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;


    public UpdateProductHandler(ILogger<UpdateProductHandler> logger, IUnitOfWork unitOfWork, IIdentityService identityService, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Check if product exists
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Payload.ProductId);
        if (product == null)
        {
            _logger.LogError("Product with id {Id} not found.", request.Payload.ProductId);
            throw new ResourceNotFoundException("Product", request.Payload.ProductId.ToString());
        }
        
        // Check if the user is the owner of the product or an admin
        _identityService.EnsureIsAdminOrOwner(product.SellerId);
        
        // Check if the server contains the image or the image is from an external sources
        if (request.Payload.ImageUpload != null && request.Payload.ImageUpload.Length > 0)
        {
            // Generate a unique filename for the image and store it.
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.Payload.ImageUpload.FileName)}";
            var filePath = Path.Combine("wwwroot/images", fileName);
            // Ensure the directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());
            // Save the image to the specified path.
            if (product.IsOwnImage)
            {
                // Delete the old image
                var oldFilePath = Path.Combine("wwwroot", product.ImageUrl);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Payload.ImageUpload.CopyToAsync(stream, cancellationToken);
            }

            // Set the image URL for the product
            request.Payload.ImageUrl = $"/images/{fileName}";
            product.IsOwnImage = true;
        }
        
        

        // Update product
        product.Name = request.Payload.Name;
        product.Description = request.Payload.Description;
        product.ImageUrl = request.Payload.ImageUrl ?? string.Empty;
        product.CategoryId = request.Payload.CategoryId;
        product.Price = request.Payload.Price;
        product.Stock = request.Payload.Stock;

        // Save changes
        product.UpdatedDate = DateTime.Now;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Send message event: IProductUpdated to Basket.Service
        await SendProductInfoUpdatedCommand(product, cancellationToken);

        return new UpdateProductResponse(product);
    }
    
    private async Task SendProductInfoUpdatedCommand(Product product, CancellationToken cancellationToken)
    {
        // var message = new IProductUpdated(product.ProductId, product.Name, product.Description, product.ImageUrl,
        //     product.CategoryId);
        var message = new
        {
            ProductId = product.ProductId,
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
        await _publishEndpoint.Publish<IProductUpdated>(message, cancellationToken);
    }
    
    
}