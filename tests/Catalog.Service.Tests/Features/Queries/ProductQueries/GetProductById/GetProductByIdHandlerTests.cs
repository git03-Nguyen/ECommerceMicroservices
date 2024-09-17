using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Repositories;
using Contracts.Exceptions;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetProductById;

[TestFixture]
public class GetProductByIdHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _handler = new GetProductByIdHandler(_unitOfWork.Object);
    }

    private Mock<IUnitOfWork> _unitOfWork;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private GetProductByIdHandler _handler;

    [Test]
    public async Task Handle_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = _fixture.Create<Product>();
        _unitOfWork.Setup(u => u.ProductRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(product);

        var query = new GetProductByIdQuery(product.ProductId);

        // Act
        var result = await _handler.Handle(query, _cancellationToken);

        // Assert
        Assert.That(result.Payload.Id, Is.EqualTo(product.ProductId));
        Assert.That(result.Payload.Name, Is.EqualTo(product.Name));
        Assert.That(result.Payload.Description, Is.EqualTo(product.Description));
    }

    [Test]
    public void Handle_ShouldThrowResourceNotFoundException_WhenProductDoesNotExist()
    {
        // Arrange
        _unitOfWork.Setup(u => u.ProductRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product)null);

        var query = new GetProductByIdQuery(1);

        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(query, _cancellationToken));
    }
}