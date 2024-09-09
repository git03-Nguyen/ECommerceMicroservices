using System.Linq.Expressions;
using Basket.Service.Features.Commands.BasketCommands.DeleteBasket;
using Contracts.Exceptions;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.DeleteBasket;

[TestFixture]
public class DeleteBasketHandlerTests
{
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<DeleteBasketHandler>> _logger;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private DeleteBasketHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<DeleteBasketHandler>>();
        
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new DeleteBasketHandler(_logger.Object, _unitOfWork.Object);
    }
    
    [Test]
    public async Task Handle_WhenGivenValidRequest_ShouldDeleteBasket()
    {
        // Arrange
        var request = _fixture.Create<DeleteBasketCommand>();
        
        _unitOfWork.Setup(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
            .Returns(new List<Data.Models.Basket> { _fixture.Create<Data.Models.Basket>() }.AsQueryable().BuildMock());
        
        // Act
        await _handler.Handle(request, _cancellationToken);
        
        // Assert
        _unitOfWork.Verify(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()), Times.Once);
        _unitOfWork.Verify(u => u.BasketRepository.RemoveRange(It.IsAny<IEnumerable<Data.Models.Basket>>()), Times.Once);
        _unitOfWork.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
        Assert.Pass();
    }
    
    [Test]
    public async Task Handle_WhenBasketNotExists_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        var request = _fixture.Create<DeleteBasketCommand>();
        
        _unitOfWork.Setup(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
            .Returns((IQueryable<Data.Models.Basket>)null);
        
        // Act & Assert
        Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(request, _cancellationToken));
    }
}