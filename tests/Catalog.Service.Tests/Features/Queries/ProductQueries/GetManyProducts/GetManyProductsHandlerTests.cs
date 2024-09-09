using System.Linq.Expressions;
using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetManyProducts;
using Catalog.Service.Repositories;
using Catalog.Service.Tests.Extensions;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetManyProducts;

[TestFixture]
public class GetManyProductsHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    private GetManyProductsHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        _handler = new GetManyProductsHandler(_unitOfWork.Object);
    }

    [Test]
    public async Task Handle_ShouldReturnGetManyProductsResponse_WhenRequestIsValid()
    {
        // Arrange
        var query = new GetManyProductsQuery(new GetManyProductsRequest
        {
            Payload = new[] { 1, 2, 3 }
        });
        var products = _fixture.CreateMany<Product>().ToList().AsQueryable().BuildMock();
        _unitOfWork.Setup(x => x.ProductRepository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
            .Returns(products);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result.Payload.Count(), Is.EqualTo(products.Count()));
        
        
    }
}