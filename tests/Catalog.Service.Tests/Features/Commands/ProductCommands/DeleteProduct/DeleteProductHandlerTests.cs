using Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;
using Catalog.Service.Repositories;
using Contracts.MassTransit.Core.SendEndpoint;
using Contracts.Services.Identity;
using ILogger = Castle.Core.Logging.ILogger;

namespace Catalog.Service.Tests.Features.Commands.ProductCommands.DeleteProduct;

[TestFixture]
public class DeleteProductHandlerTests
{
    private Mock<ILogger<DeleteProductHandler>> _logger;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<IIdentityService> _identityService;
    private Mock<ISendEndpointCustomProvider> _sendEndpointCustomProvider;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private DeleteProductHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<DeleteProductHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _identityService = new Mock<IIdentityService>();
        _identityService.Setup(i => i.EnsureIsAdminOrOwner(It.IsAny<Guid>()));
        _sendEndpointCustomProvider = new Mock<ISendEndpointCustomProvider>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new DeleteProductHandler(_logger.Object, _unitOfWork.Object, _identityService.Object, _sendEndpointCustomProvider.Object);
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldDeleteProduct()
    {
        // Arrange
        var command = _fixture.Create<DeleteProductCommand>();
        var product = _fixture.Create<Data.Models.Product>();
        
        _unitOfWork.Setup(u => u.ProductRepository.GetByIdAsync(command.Id)).ReturnsAsync(product);
        
        // Act
        await _handler.Handle(command, _cancellationToken);
        
        // Assert
        _unitOfWork.Verify(u => u.ProductRepository.Remove(product), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
    }
    
    
}