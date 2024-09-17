using System.Linq.Expressions;
using Basket.Service.Data.Models;
using Basket.Service.Features.Commands.ProductCommands.UpdateProduct;

namespace Basket.Service.Tests.Features.Commands.ProductCommands.UpdateProductInfo;

[TestFixture]
public class UpdateProductHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<UpdateProductHandler>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _handler = new UpdateProductHandler(_unitOfWorkMock.Object, _loggerMock.Object);
    }

    private Mock<ILogger<UpdateProductHandler>> _loggerMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private UpdateProductHandler _handler;

    [Test]
    public async Task Handle_WhenCalled_ShouldUpdateProductInfo()
    {
        // Arrange
        var command = _fixture.Create<UpdateProductCommand>();
        var basketItems = _fixture.CreateMany<BasketItem>().ToList();
        var basketItemsMock = basketItems.AsQueryable().BuildMock();

        _unitOfWorkMock
            .Setup(u => u.BasketItemRepository.GetByCondition(It.IsAny<Expression<Func<BasketItem, bool>>>()))
            .Returns(basketItemsMock);

        // Act
        await _handler.Handle(command, _cancellationToken);

        // Assert
        _unitOfWorkMock.Verify(u => u.BasketItemRepository.UpdateRange(basketItems), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(_cancellationToken), Times.Once);
    }
}