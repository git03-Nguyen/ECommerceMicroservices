using Catalog.Service.Data.Models;
using Catalog.Service.Features.Queries.CategoryQueries.GetCategories;
using Catalog.Service.Repositories;
using Catalog.Service.Tests.Extensions;

namespace Catalog.Service.Tests.Features.Queries.CategoryQueries.GetCategories;

[TestFixture]
public class GetAllCategoriesHandlerTests
{
    private Mock<IUnitOfWork> _catalogUnitOfWork;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private GetAllCategoriesHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _catalogUnitOfWork = new Mock<IUnitOfWork>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new GetAllCategoriesHandler(_catalogUnitOfWork.Object);
    }
    
    [Test]
    public async Task Handle_ShouldReturnAllCategories()
    {
        // Arrange
        var categories = _fixture.CreateMany<Category>().ToList().AsQueryable().BuildMock();
        _catalogUnitOfWork.Setup(u => u.CategoryRepository.GetAll()).Returns(categories.AsQueryable());
        
        var query = _fixture.Create<GetAllCategoriesQuery>();
        
        // Act
        var response = await _handler.Handle(query, _cancellationToken);
        
        // Assert
        Assert.That(response.Payload, Is.EquivalentTo(categories));
    }

    [Test]
    public async Task Handle_WhenEmpty_ShouldReturnEmptyList()
    {
        // Arrange
        var categories = new List<Category>().AsQueryable().BuildMock();
        _catalogUnitOfWork.Setup(u => u.CategoryRepository.GetAll()).Returns(categories.AsQueryable());
        
        var query = _fixture.Create<GetAllCategoriesQuery>();
        
        // Act
        var response = await _handler.Handle(query, _cancellationToken);
        
        // Assert
        Assert.That(response.Payload, Is.EquivalentTo(categories));
    }
    
}