using System.Linq.Expressions;
using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetProducts;
using Catalog.Service.Models.Dtos;
using Catalog.Service.Models.Filters;
using Catalog.Service.Repositories;
using Contracts.Services.Identity;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetProducts;

[TestFixture]
public class GetProductsHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _identityService = new Mock<IIdentityService>();
        _handler = new GetProductsHandler(_unitOfWork.Object, _identityService.Object);
    }

    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IIdentityService> _identityService;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private GetProductsHandler _handler;

    [Test]
    public async Task Handle_ShouldReturnProducts()
    {
        // Arrange
        var request = new GetProductsQuery(new GetProductsRequest
        {
            CategoryIds = "1",
            PageNumber = 1,
            PageSize = 10,
            SortBy = nameof(Product.ProductId),
            SortOrder = FilterConstants.Ascending,
            MinPrice = 1,
            MaxPrice = 100
        });

        var products = new List<Product>
        {
            new() { ProductId = 1, CategoryId = 1, Name = "Product 1", Price = 10 },
            new() { ProductId = 2, CategoryId = 1, Name = "Product 2", Price = 20 }
        }.AsQueryable().BuildMock();
        var productDtos = products.Select(p => new ProductDto(p)).ToList();

        _unitOfWork.Setup(u => u.ProductRepository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
            .Returns(products.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(request, _cancellationToken);

        // Assert
        Assert.That(result.Payload.Count(), Is.EqualTo(productDtos.Count));

        _unitOfWork.Verify(u => u.ProductRepository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>()),
            Times.Once);
    }
}