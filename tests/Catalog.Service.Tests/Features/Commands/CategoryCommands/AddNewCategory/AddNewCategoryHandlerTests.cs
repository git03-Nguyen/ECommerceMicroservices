using Catalog.Service.Data.Models;
using Catalog.Service.Features.Commands.CategoryCommands.AddNewCategory;
using Catalog.Service.Repositories;
using Contracts.Exceptions;
using Contracts.Services.Identity;

namespace Catalog.Service.Tests.Features.Commands.CategoryCommands.AddNewCategory;

[TestFixture]
public class AddNewCategoryHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger<AddNewCategoryHandler>>();
        _identityService = new Mock<IIdentityService>();
        _identityService.Setup(x => x.EnsureIsAdmin());

        _fixture = new Fixture().OmitOnRecursionBehavior();
        _cancellationToken = new CancellationToken();

        _handler = new AddNewCategoryHandler(_unitOfWork.Object, _logger.Object, _identityService.Object);
    }

    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger<AddNewCategoryHandler>> _logger;
    private Mock<IIdentityService> _identityService;

    private Fixture _fixture;
    private CancellationToken _cancellationToken;

    private AddNewCategoryHandler _handler;

    [Test]
    public async Task Handle_WhenCalled_ShouldReturnAddNewCategoryResponse()
    {
        // Arrange
        var request = new AddNewCategoryCommand(_fixture.Create<AddNewCategoryRequest>());
        var category = _fixture.Create<Category>();

        _unitOfWork.Setup(x => x.CategoryRepository.AddAsync(It.IsAny<Category>())).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, _cancellationToken);

        // Assert
        Assert.That(result, Is.Not.Null);
        _unitOfWork.Verify(x => x.SaveChangesAsync(_cancellationToken), Times.Once);
    }

    [Test]
    public async Task Handle_WhenFailedToAddCategory_ShouldThrowException()
    {
        // Arrange
        var request = new AddNewCategoryCommand(_fixture.Create<AddNewCategoryRequest>());

        _unitOfWork.Setup(x => x.CategoryRepository.AddAsync(It.IsAny<Category>())).ReturnsAsync(false);

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, _cancellationToken));
    }

    [Test]
    public async Task Handle_WhenNotAdmin_ShouldThrowException()
    {
        // Arrange
        var request = new AddNewCategoryCommand(_fixture.Create<AddNewCategoryRequest>());

        _identityService.Setup(x => x.EnsureIsAdmin()).Throws<UnAuthorizedAccessException>();

        // Act & Assert
        Assert.ThrowsAsync<UnAuthorizedAccessException>(() => _handler.Handle(request, _cancellationToken));
    }
}