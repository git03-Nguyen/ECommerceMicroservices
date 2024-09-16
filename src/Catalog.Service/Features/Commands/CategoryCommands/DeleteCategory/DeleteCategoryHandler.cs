using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.MassTransit.Messages.Commands;
using Contracts.Services.Identity;
using MassTransit;
using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ILogger<DeleteCategoryHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategoryHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<int> productIds;
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null) throw new ResourceNotFoundException(nameof(Category), request.CategoryId.ToString());

            // Delete all products in this category
            var products = _unitOfWork.ProductRepository.GetByCondition(x => x.CategoryId == request.CategoryId);
            productIds = products.Select(x => x.ProductId);
            _unitOfWork.ProductRepository.RemoveRange(products);

            // Delete the category
            _unitOfWork.CategoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete category");
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        // Send command: DeleteProducts
        await SendDeleteProductsCommand(productIds, cancellationToken);

        return true;
    }

    private async Task SendDeleteProductsCommand(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        var message = new
        {
            ProductIds = productIds.ToList()
        };
        await _publishEndpoint.Publish<IDeleteProducts>(message, cancellationToken);
    }
}