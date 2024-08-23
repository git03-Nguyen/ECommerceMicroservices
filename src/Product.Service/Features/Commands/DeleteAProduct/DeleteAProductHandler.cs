using MediatR;
using Product.Service.Features.Queries.GetProductById;
using Product.Service.Models;
using Product.Service.Repositories;

namespace Product.Service.Features.Commands.DeleteAProduct;

public class DeleteAProductHandler : IRequestHandler<DeleteAProductCommand>
{
    private readonly IProductItemRepository _productItemRepository;
    
    public DeleteAProductHandler(IProductItemRepository productItemRepository)
    {
        _productItemRepository = productItemRepository;
    }
    
    public async Task Handle(DeleteAProductCommand request, CancellationToken cancellationToken)
    {
        var result =  await _productItemRepository.Delete(request.Id);
        if (!result)
        {
            throw new ProductNotDeleteException(nameof(ProductItem), request.Id);
        }
    }
    
}

public class ProductNotDeleteException : Exception
{
    public ProductNotDeleteException(string productItemName, Guid requestId)
    {
        ProductItemName = productItemName;
        RequestId = requestId;
    }
    
    public string ProductItemName { get; }
    public Guid RequestId { get; }
}