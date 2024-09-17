using Catalog.Service.Features.Commands.ProductCommands.DeleteProduct;

namespace Catalog.Service.Tests.Features.Commands.ProductCommands.DeleteProduct;

[TestFixture]
public class DeleteProductValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _validator = new DeleteProductValidator();
        _fixture = new Fixture();
        _command = new DeleteProductCommand(1);
    }

    private DeleteProductValidator _validator;
    private Fixture _fixture;
    private DeleteProductCommand _command;

    private static IEnumerable<TestCaseData> InvalidIdTestCases()
    {
        yield return new TestCaseData(null).SetName("Id is null");
        yield return new TestCaseData(0).SetName("Id is 0");
        yield return new TestCaseData(-1).SetName("Id is negative");
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

    [Test]
    [TestCaseSource(nameof(InvalidIdTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidId(int id)
    {
        // Arrange
        _command.Id = id;

        // Act
        var result = await _validator.ValidateAsync(_command, CancellationToken.None);

        // Assert
        Assert.That(result.IsValid, Is.False);
    }
}