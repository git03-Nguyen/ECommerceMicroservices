using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.CategoryCommands.UpdateCategory;
using Catalog.Service.Repositories;
using Catalog.Service.Repositories.Interfaces;
using Contracts.Exceptions;
using Contracts.Services.Identity;

namespace Catalog.Service.Tests.Features.Commands.CategoryCommands.UpdateCategory;

[TestFixture]
public class UpdateCategoryHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IIdentityService> _identityService;
    private Mock<ILogger<UpdateCategoryHandler>> _logger;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private UpdateCategoryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _identityService = new Mock<IIdentityService>();
        _identityService.Setup(x => x.EnsureIsAdmin());
        _logger = new Mock<ILogger<UpdateCategoryHandler>>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new UpdateCategoryHandler(_logger.Object, _unitOfWork.Object, _identityService.Object);
    }

    [Test]
    public async Task Handle_WhenNotAdmin_ShouldThrowException()
    {
        // Arrange
        _identityService.Setup(x => x.EnsureIsAdmin()).Throws<ForbiddenAccessException>();
        
        var request = _fixture.Create<UpdateCategoryCommand>();
        
        // Act & Assert
        Assert.ThrowsAsync<ForbiddenAccessException>(() => _handler.Handle(request, _cancellationToken));
    }
    
    [Test]
    public async Task Handle_WhenCategoryNotFound_ShouldThrowException()
    {
        // Arrange
        var request = _fixture.Create<UpdateCategoryCommand>();
        
        _unitOfWork.Setup(u => u.CategoryRepository.GetByIdAsync(request.Payload.CategoryId)).ReturnsAsync((Category)null);
        
        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(request, _cancellationToken));
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldUpdateCategory()
    {
        // Arrange
        var request = _fixture.Create<UpdateCategoryCommand>();
        var category = _fixture.Create<Category>();
        
        _unitOfWork.Setup(u => u.CategoryRepository.GetByIdAsync(request.Payload.CategoryId)).ReturnsAsync(category);
        
        // Act
        var result = await _handler.Handle(request, _cancellationToken);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<UpdateCategoryResponse>());
        Assert.That(result.CategoryId, Is.EqualTo(category.CategoryId));
        Assert.That(result.Name, Is.EqualTo(request.Payload.Name ?? category.Name));
        Assert.That(result.Description, Is.EqualTo(request.Payload.Description ?? category.Description));
        Assert.That(result.ImageUrl, Is.EqualTo(request.Payload.ImageUrl ?? category.ImageUrl));
        _unitOfWork.Verify(u => u.CategoryRepository.Update(category), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
    }
}