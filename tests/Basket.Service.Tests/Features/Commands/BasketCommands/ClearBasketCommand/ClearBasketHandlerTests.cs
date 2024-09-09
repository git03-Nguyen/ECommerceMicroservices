using Basket.Service.Features.Commands.BasketCommands.ClearBasketAfterOrderCreated;
using Contracts.Exceptions;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.ClearBasketCommand;

[TestFixture]
public class ClearBasketHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<ClearBasketAfterOrderCreatedHandler>> _logger;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private ClearBasketAfterOrderCreatedHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<ClearBasketAfterOrderCreatedHandler>>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new ClearBasketAfterOrderCreatedHandler(_unitOfWork.Object, _logger.Object);
    }
    
    [Test]
    public async Task Handle_ShouldClearBasket_WhenGivenValidRequest()
    {
        // Arrange
        var request = _fixture.Create<ClearBasketAfterOrderCreatedCommand>();
        
        _unitOfWork.Setup(u => u.BasketRepository.GetByIdAsync(request.Payload.BasketId))
            .ReturnsAsync(_fixture.Create<Data.Models.Basket>());
        
        // Act
        await _handler.Handle(request, _cancellationToken);
        
        // Assert
        _unitOfWork.Verify(u => u.BasketRepository.GetByIdAsync(request.Payload.BasketId), Times.Once);
        _unitOfWork.Verify(u => u.BasketRepository.Update(It.IsAny<Data.Models.Basket>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
        Assert.Pass();
    }
    
    [Test]
    public async Task Handle_WhenBasketNotExists_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        var request = _fixture.Create<ClearBasketAfterOrderCreatedCommand>();
        
        _unitOfWork.Setup(u => u.BasketRepository.GetByIdAsync(request.Payload.BasketId))
            .ReturnsAsync((Data.Models.Basket)null);
        
        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(request, _cancellationToken));
    }
    
}