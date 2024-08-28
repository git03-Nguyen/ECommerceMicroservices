// using Catalog.Service.Models.Responses;
// using Catalog.Service.Repositories.Interfaces;
// using MediatR;
//
// namespace Catalog.Service.Features.Commands.ProductCommands.CreateNewProduct;
//
// public class CreateNewProductHandler : IRequestHandler<CreateNewProductCommand, AddNewProductResponse>
// {
//     private readonly IProductRepository _productRepository;
//
//     public CreateNewProductHandler(IProductRepository productRepository)
//     {
//         _productRepository = productRepository;
//     }
//
//     public async Task<AddNewProductResponse> Handle(CreateNewProductCommand request, CancellationToken cancellationToken)
//     {
//         var product = new Data.Models.Product
//         {
//             Name = request.Payload.Name,
//             Description = request.Payload.Description,
//             Price = request.Payload.Price,
//             CategoryId = request.Payload.CategoryId
//         };
//         
//         var createdProduct = await _productRepository.Create(product);
//         
//         var newProductResponse = new AddNewProductResponse
//         {
//             ProductId = product.ProductId,
//             CreatedDate = product.CreatedDate,
//         };
//         
//         return newProductResponse;
//     }
// }