using Catalog.Service.Features.Commands.CategoryCommands.DeleteCategory;

namespace Catalog.Service.Tests.Features.Commands.CategoryCommands.DeleteCategory;

[TestFixture]
public class DeleteCategoryValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new DeleteCategoryValidator();
        _command = new DeleteCategoryCommand(1);
    }

    private Fixture _fixture;
    private DeleteCategoryValidator _validator;
    private DeleteCategoryCommand _command;

    private static IEnumerable<TestCaseData> InvalidCategoryIdTestCases()
    {
        yield return new TestCaseData(0).SetName("CategoryId is 0");
        yield return new TestCaseData(null).SetName("CategoryId is null");
        yield return new TestCaseData(-1).SetName("CategoryId is negative");
    }

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);

        // Assert
        Assert.That(result.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidCategoryIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidCategoryId(int categoryId)
    {
        // Arrange
        _command.CategoryId = categoryId;

        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);

        // Assert
        Assert.That(result.IsValid, Is.False);
    }
}