using System.Linq.Expressions;
using Basket.Service.Features.Commands.BasketCommands.UpdateStockAfterOrderCreated;
using Contracts.Exceptions;
using Contracts.MassTransit.Messages.Events;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.UpdateStock;

[TestFixture]
public class UpdateStockHandlerTests
{
    private UpdateStockAfterOrderCreatedHandler _handler;
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<UpdateStockAfterOrderCreatedHandler>> _logger;
    private UpdateStockAfterOrderCreatedCommand _command;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<UpdateStockAfterOrderCreatedHandler>>();
        _handler = new UpdateStockAfterOrderCreatedHandler(_logger.Object, _unitOfWork.Object);
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _command = new UpdateStockAfterOrderCreatedCommand(_fixture.Create<OrderCreated>());
    }

    // [Test]
    // public async Task Handle_ShouldUpdateStock_WhenGivenValidRequest()
    // {
    //     // Arrange
    //     _command.Basket = new OrderCreated
    //     {
    //         BasketId = 1,
    //         OrderItems = new List<OrderItemCreated>
    //         {
    //             new OrderItemCreated
    //             {
    //                 ProductId = 1,
    //                 Quantity = 1
    //             }
    //         }
    //     };
    //     _unitOfWork.Setup(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()))
    //         .Returns(new List<Data.Models.Basket> { _fixture.Create<Data.Models.Basket>() }.AsQueryable().BuildMock());
    //
    //     // Act
    //     await _handler.Handle(_command, new CancellationToken());
    //
    //     // Assert
    //     _unitOfWork.Verify(u => u.BasketRepository.GetByCondition(It.IsAny<Expression<Func<Data.Models.Basket, bool>>>()), Times.Once);
    //     _unitOfWork.Verify(u => u.BasketRepository.Update(It.IsAny<Data.Models.Basket>()), Times.Once);
    //     _unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    // }
}