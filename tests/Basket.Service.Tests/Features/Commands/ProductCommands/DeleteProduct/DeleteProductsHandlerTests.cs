using System.Linq.Expressions;
using Basket.Service.Data.Models;
using Basket.Service.Features.Commands.ProductCommands.DeleteProduct;

namespace Basket.Service.Tests.Features.Commands.ProductCommands.DeleteProduct;

[TestFixture]
public class DeleteProductsHandlerTests
{
    private Mock<ILogger<DeleteProductsHandler>> _loggerMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private DeleteProductsHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<DeleteProductsHandler>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new DeleteProductsHandler(_loggerMock.Object, _unitOfWorkMock.Object);
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldRemoveBasketItems()
    {
        // Arrange
        var command = _fixture.Create<DeleteProductsCommand>();
        var basketItems = _fixture.CreateMany<BasketItem>().ToList();
        var basketItemsMock = basketItems.AsQueryable().BuildMock();
        
        _unitOfWorkMock.Setup(u => u.BasketItemRepository.GetByCondition(It.IsAny<Expression<Func<BasketItem, bool>>>()))
            .Returns(basketItemsMock);
        
        // Act
        await _handler.Handle(command, _cancellationToken);
        
        // Assert
        _unitOfWorkMock.Verify(u => u.BasketItemRepository.RemoveRange(basketItems), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
    }
    
}