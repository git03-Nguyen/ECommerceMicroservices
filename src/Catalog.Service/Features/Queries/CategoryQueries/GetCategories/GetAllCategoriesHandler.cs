using Catalog.Service.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategories;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.CategoryRepository.GetAll().OrderBy(c => c.CategoryId)
            .ToListAsync(cancellationToken);
        return new GetAllCategoriesResponse(categories);
    }
}