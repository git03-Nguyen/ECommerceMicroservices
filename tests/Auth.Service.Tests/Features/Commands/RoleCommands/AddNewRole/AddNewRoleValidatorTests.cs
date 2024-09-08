using Auth.Service.Features.Commands.RoleCommands.AddNewRole;

namespace Auth.Service.Tests.Features.Commands.RoleCommands.AddNewRole;

[TestFixture]
public class AddNewRoleValidatorTests
{
    private AddNewRoleValidator _validator;
    private AddNewRoleRequest _request;

    [SetUp]
    public void SetUp()
    {
        _validator = new AddNewRoleValidator();
        _request = new AddNewRoleRequest
        {
            Name = "Valid Role Name"
        };
    }

    #region Setup Test Cases

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
        
        var nameOverMaximumLength = new string('a', 101);
        yield return new TestCaseData(nameOverMaximumLength)
            .SetName("Name is over maximum length");
    }
    
    #endregion

    #region Setup Tests

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new AddNewRoleCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalid_Name(string invalidName)
    {
        // Arrange
        var request = new AddNewRoleRequest
        {
            Name = invalidName
        };
        
        var command = new AddNewRoleCommand(request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    

    #endregion
    
}