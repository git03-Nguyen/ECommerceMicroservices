using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.CategoryQueries.GetCategoryById;
using Catalog.Service.Repositories;
using Catalog.Service.Tests.Extensions;
using Contracts.Exceptions;

namespace Catalog.Service.Tests.Features.Queries.CategoryQueries.GetCategoryById;

[TestFixture]
public class GetCategoryByIdHandlerTests
{
    private Mock<ICatalogUnitOfWork> _catalogUnitOfWork;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private GetCategoryByIdHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _catalogUnitOfWork = new Mock<ICatalogUnitOfWork>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new GetCategoryByIdHandler(_catalogUnitOfWork.Object);
    }
    
    [Test]
    public async Task Handle_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var category = _fixture.Create<Category>();
        _catalogUnitOfWork.Setup(u => u.CategoryRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(category);
        
        var query = new GetCategoryByIdQuery(category.CategoryId);
        
        // Act
        var result = await _handler.Handle(query, _cancellationToken);
        
        // Assert
        Assert.That(result.Payload.CategoryId, Is.EqualTo(category.CategoryId));
        Assert.That(result.Payload.Name, Is.EqualTo(category.Name));
        Assert.That(result.Payload.Description, Is.EqualTo(category.Description));
    }
    
    [Test]
    public void Handle_ShouldThrowResourceNotFoundException_WhenCategoryDoesNotExist()
    {
        // Arrange
        _catalogUnitOfWork.Setup(u => u.CategoryRepository.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Category)null);
        
        var query = new GetCategoryByIdQuery(1);
        
        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(query, _cancellationToken));
        
    }
    
    
    
}