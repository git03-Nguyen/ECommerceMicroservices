using Catalog.Service.Data.Models;
using Catalog.Service.Models.Requests;
using Catalog.Service.Models.Responses;
using MediatR;

namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;

public class CreateNewProductCommand : IRequest<CreateNewProductResponse>
{
    public CreateNewProductRequest Request { get; set; }
    
    public CreateNewProductCommand(CreateNewProductRequest request)
    {
        Request = request;
    }
    
}