using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.ProductQueries.GetProductById;
using Catalog.Service.Repositories;
using Catalog.Service.Tests.Extensions;
using Contracts.Exceptions;

namespace Catalog.Service.Tests.Features.Queries.ProductQueries.GetProductById;

[TestFixture]
public class GetProductByIdHandlerTests
{
    private Mock<ICatalogUnitOfWork> _unitOfWork;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private GetProductByIdHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<ICatalogUnitOfWork>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new GetProductByIdHandler(_unitOfWork.Object);
    }
    
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
        Assert.That(result.Payload.ProductId, Is.EqualTo(product.ProductId));
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