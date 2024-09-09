using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Services.Identity;
using MediatR;

namespace Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;

public class AddNewCategoryHandler : IRequestHandler<AddNewCategoryCommand, AddNewCategoryResponse>
{
    private readonly ILogger<AddNewCategoryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public AddNewCategoryHandler(IUnitOfWork unitOfWork, ILogger<AddNewCategoryHandler> logger, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _identityService = identityService;
    }

    public async Task<AddNewCategoryResponse> Handle(AddNewCategoryCommand request, CancellationToken cancellationToken)
    {
        // Check if admin
        _identityService.EnsureIsAdmin();
        
        var category = new Category
        {
            Name = request.Payload.Name,
            Description = request.Payload.Description,
            ImageUrl = request.Payload.ImageUrl
        };

        var success = await _unitOfWork.CategoryRepository.AddAsync(category);
        if (!success) throw new Exception("Failed to add new category");

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AddNewCategoryResponse(category);
    }
}