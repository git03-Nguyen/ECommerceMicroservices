using Catalog.Service.Data.Models;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using MediatR;

namespace Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null) throw new ResourceNotFoundException(nameof(Category), request.CategoryId.ToString());

        return new GetCategoryByIdResponse(category);
    }
}
