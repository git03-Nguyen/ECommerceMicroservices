using System.Linq.Expressions;
using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.Services.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Service.Tests.Features.Commands.CategoryCommands.DeleteCategory;

[TestFixture]
public class DeleteCategoryHandlerTests
{
    private Mock<ILogger<DeleteCategoryHandler>> _logger;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IIdentityService> _identityService;
    private Mock<ISendEndpointCustomProvider> _sendEndpointCustomProvider;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private DeleteCategoryHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<DeleteCategoryHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _identityService = new Mock<IIdentityService>();
        _identityService.Setup(x => x.EnsureIsAdmin());
        _sendEndpointCustomProvider = new Mock<ISendEndpointCustomProvider>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new DeleteCategoryHandler(_unitOfWork.Object, _logger.Object, _identityService.Object, _sendEndpointCustomProvider.Object);
    }
    
    
    [Test]
    public async Task Handle_WhenCalled_ShouldReturnTrue()
    {
        // Arrange
        var command = new DeleteCategoryCommand(1);
        
        _unitOfWork.Setup(x => x.BeginTransactionAsync(_cancellationToken)).ReturnsAsync(new Mock<IDbContextTransaction>().Object);
        _unitOfWork.Setup(x => x.CategoryRepository.GetByIdAsync(command.CategoryId)).ReturnsAsync(new Category());
        _unitOfWork.Setup(x => x.ProductRepository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>().AsQueryable().BuildMock());
        _unitOfWork.Setup(x => x.SaveChangesAsync(_cancellationToken));
        
        // Act
        var result = await _handler.Handle(command, _cancellationToken);
        
        // Assert
        Assert.That(result, Is.True);
        _unitOfWork.Verify(x => x.SaveChangesAsync(_cancellationToken), Times.Once);
    }
    
    [Test]
    public async Task Handle_WhenCategoryNotFound_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        var command = new DeleteCategoryCommand(1);
        
        _unitOfWork.Setup(x => x.BeginTransactionAsync(_cancellationToken)).ReturnsAsync(new Mock<IDbContextTransaction>().Object);
        _unitOfWork.Setup(x => x.CategoryRepository.GetByIdAsync(command.CategoryId)).ReturnsAsync((Category)null);
        
        // Act & Assert
        var exception = Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(command, _cancellationToken));
    }
    
    [Test]
    public async Task Handle_WhenNotAdmin_ShouldThrowException()
    {
        // Arrange
        var command = new DeleteCategoryCommand(1);
        
        _identityService.Setup(x => x.EnsureIsAdmin()).Throws<ForbiddenAccessException>();
        
        // Act & Assert
        var exception = Assert.ThrowsAsync<ForbiddenAccessException>(() => _handler.Handle(command, _cancellationToken));
    }
    
    
}
