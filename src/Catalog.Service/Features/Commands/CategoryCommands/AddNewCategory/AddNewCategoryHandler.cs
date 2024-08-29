using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Interfaces;
using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryHandler : IRequestHandler<AddNewCategoryCommand, AddNewCategoryResponse>
{
    private readonly ICatalogUnitOfWork _unitOfWork;

    public AddNewCategoryHandler(ICatalogUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddNewCategoryResponse> Handle(AddNewCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Payload.Name,
            Description = request.Payload.Description,
            ImageUrl = request.Payload.ImageUrl
        };

        // TODO: any cancelation token handling here?
        var success = await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!success)
        {
            throw new Exception("Failed to add new category");
        }

        return new AddNewCategoryResponse(category);
    }
}