using Auth.Service.Features.Commands.UserCommands.LogIn;
using Auth.Service.Features.Queries.UserQueries.GetUserById;
using Auth.Service.Tests.Extensions;
using FluentValidation.TestHelper;

namespace Auth.Service.Tests.Features.Commands.UserCommands.Login;

[TestFixture]
public class LogInValidatorTests
{
    private LogInValidator _validator;
    private LogInRequest _request;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new LogInValidator();
        _request = new LogInRequest
        {
            UserName = "admin",
            Password = "Admin@123"
        };
    }

    #region Setup Test Cases

    private static IEnumerable<TestCaseData> InvalidUserNameTestCases()
    {
        yield return new TestCaseData(null).SetName("UserName is null");
        yield return new TestCaseData(string.Empty).SetName("UserName is empty");
        yield return new TestCaseData(" ").SetName("UserName is whitespace");
    }
    
    private static IEnumerable<TestCaseData> InvalidPasswordTestCases()
    {
        yield return new TestCaseData(null).SetName("Password is null");
        yield return new TestCaseData(string.Empty).SetName("Password is empty");
        yield return new TestCaseData(" ").SetName("Password is whitespace");
    }
    
    #endregion

    #region Setup Tests
    
    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new LogInCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.True);
    }
    
    [TestCaseSource(nameof(InvalidUserNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRequest(string userName)
    {
        // Arrange
        _request.UserName = userName;
        var command = new LogInCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
    
    [TestCaseSource(nameof(InvalidPasswordTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidPassword(string password)
    {
        // Arrange
        _request.Password = password;
        var command = new LogInCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);
        
        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
#endregion   

}