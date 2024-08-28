using Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;
using Catalog.Service.Repositories;
using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ILogger<DeleteCategoryHandler> _logger;
    private readonly ICatalogUnitOfWork _unitOfWork;
    
    public DeleteCategoryHandler(ICatalogUnitOfWork unitOfWork, ILogger<DeleteCategoryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.CategoryId);
            }
            
            // Delete all products in this category
            var products = _unitOfWork.ProductRepository.GetByCondition(x => x.CategoryId == request.CategoryId);
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
        
        return true;
    }
}