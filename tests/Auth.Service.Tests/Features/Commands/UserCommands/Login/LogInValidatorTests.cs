using Auth.Service.Features.Commands.UserCommands.LogIn;

namespace Auth.Service.Tests.Features.Commands.UserCommands.Login;

[TestFixture]
public class LogInValidatorTests
{
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

    private LogInValidator _validator;
    private LogInRequest _request;
    private Fixture _fixture;

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
}