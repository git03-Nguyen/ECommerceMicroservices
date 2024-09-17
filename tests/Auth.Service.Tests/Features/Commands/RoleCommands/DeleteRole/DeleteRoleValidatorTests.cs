using Auth.Service.Features.Commands.RoleCommands.DeleteRole;

namespace Auth.Service.Tests.Features.Commands.RoleCommands.DeleteRole;

[TestFixture]
public class DeleteRoleValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _validator = new DeleteRoleValidator();
        _request = new DeleteRoleRequest
        {
            Name = "Valid Role Name"
        };
    }

    private DeleteRoleValidator _validator;
    private DeleteRoleRequest _request;

    private static IEnumerable<TestCaseData> InvalidNameTestCases()
    {
        yield return new TestCaseData(null)
            .SetName("Name is null");

        yield return new TestCaseData(string.Empty)
            .SetName("Name is empty");

        yield return new TestCaseData(" ")
            .SetName("Name is whitespace");

        yield return new TestCaseData("Name@")
            .SetName("Name contains special characters");

        var nameOverMaximumLength = new string('a', 21);
        yield return new TestCaseData(nameOverMaximumLength)
            .SetName("Name is over maximum length");
    }

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new DeleteRoleCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalid_Name(string invalidName)
    {
        // Arrange
        var request = new DeleteRoleRequest
        {
            Name = invalidName
        };

        var command = new DeleteRoleCommand(request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
}