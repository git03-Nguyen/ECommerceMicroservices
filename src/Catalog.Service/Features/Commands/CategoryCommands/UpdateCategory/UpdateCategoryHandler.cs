using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.Services.Identity;
using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly ILogger<UpdateCategoryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryHandler(ILogger<UpdateCategoryHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Payload.CategoryId);
        if (category == null)
            throw new ResourceNotFoundException(nameof(Category), request.Payload.CategoryId.ToString());

        if (request.Payload.Name != null) category.Name = request.Payload.Name;
        if (request.Payload.Description != null) category.Description = request.Payload.Description;
        category.UpdatedDate = DateTime.UtcNow;

        _unitOfWork.CategoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryResponse(category);
    }
}