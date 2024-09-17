using Auth.Service.Features.Commands.RoleCommands.UpdateRole;

namespace Auth.Service.Tests.Features.Commands.RoleCommands.UpdateRole;

[TestFixture]
public class UpdateRoleValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _validator = new UpdateRoleValidator();
        _request = new UpdateRoleRequest
        {
            Name = "Valid Role Name",
            NewName = "Valid New Role Name"
        };
    }

    private UpdateRoleValidator _validator;
    private UpdateRoleRequest _request;

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

    private static IEnumerable<TestCaseData> InvalidNewNameTestCases()
    {
        yield return new TestCaseData(null)
            .SetName("NewName is null");

        yield return new TestCaseData(string.Empty)
            .SetName("NewName is empty");

        yield return new TestCaseData(" ")
            .SetName("NewName is whitespace");

        yield return new TestCaseData("Name@")
            .SetName("NewName contains special characters");

        var nameOverMaximumLength = new string('a', 21);
        yield return new TestCaseData(nameOverMaximumLength)
            .SetName("NewName is over maximum length");
    }

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new UpdateRoleCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalid_Name(string invalidName)
    {
        // Arrange
        var request = new UpdateRoleRequest
        {
            Name = invalidName,
            NewName = "Valid New Role Name"
        };

        var command = new UpdateRoleCommand(request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidNewNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalid_NewName(string invalidNewName)
    {
        // Arrange
        var request = new UpdateRoleRequest
        {
            Name = "Valid Role Name",
            NewName = invalidNewName
        };

        var command = new UpdateRoleCommand(request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
}