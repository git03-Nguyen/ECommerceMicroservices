using System.Linq.Expressions;
using Basket.Service.Data.Models;
using Basket.Service.Features.Commands.ProductCommands.UpdateProductPriceStock;

namespace Basket.Service.Tests.Features.Commands.ProductCommands.UpdateProductPriceStock;

[TestFixture]
public class UpdateProductPriceStockHandlerTests
{
    private Mock<ILogger<UpdateProductPriceStockHandler>> _loggerMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    
    private Fixture _fixture;
    private CancellationToken _cancellationToken;
    
    private UpdateProductPriceStockHandler _handler;
    
    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<UpdateProductPriceStockHandler>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();
        
        _handler = new UpdateProductPriceStockHandler(_loggerMock.Object, _unitOfWorkMock.Object);
    }
    
    [Test]
    public async Task Handle_WhenCalled_ShouldUpdateProductPriceAndStock()
    {
        // Arrange
        var command = _fixture.Create<UpdateProductPriceStockCommand>();
        var basketItems = _fixture.CreateMany<BasketItem>().ToList();
        var basketItemsMock = basketItems.AsQueryable().BuildMock();
        
        _unitOfWorkMock.Setup(x => x.BasketItemRepository.GetByCondition(It.IsAny<Expression<Func<BasketItem, bool>>>()))
            .Returns(basketItemsMock);
        
        // Act
        await _handler.Handle(command, _cancellationToken);
        
        // Assert
        _unitOfWorkMock.Verify(x => x.BasketItemRepository.UpdateRange(basketItems), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(_cancellationToken), Times.Once);
    }

}