using Auth.Service.Features.Commands.UserCommands.ChangePassword;

namespace Auth.Service.Tests.Features.Commands.UserCommands.ChangePassword;

public class ChangePasswordValidatorTests
{
    private Fixture _fixture;
    private ChangePasswordRequest _request;
    private ChangePasswordValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().OmitOnRecursionBehavior();
        _validator = new ChangePasswordValidator();
        _request = new ChangePasswordRequest
        {
            Password = "Admin@123",
            NewPassword = "Admin@1234"
        };
    }

    #region Setup Test Cases

    private static IEnumerable<TestCaseData> InvalidPasswordTestCases()
    {
        yield return new TestCaseData(null).SetName("Password is null");
        yield return new TestCaseData(string.Empty).SetName("Password is empty");
        yield return new TestCaseData(" ").SetName("Password is whitespace");
        yield return new TestCaseData("a").SetName("Password is less than 6 characters");
        yield return new TestCaseData("pass word").SetName("Password contains whitespace");
    }

    private static IEnumerable<TestCaseData> InvalidNewPasswordTestCases()
    {
        yield return new TestCaseData(null).SetName("NewPassword is null");
        yield return new TestCaseData(string.Empty).SetName("NewPassword is empty");
        yield return new TestCaseData(" ").SetName("NewPassword is whitespace");
        yield return new TestCaseData("a").SetName("NewPassword is less than 6 characters");
        yield return new TestCaseData("pass word").SetName("NewPassword contains whitespace");
    }

    #endregion

    #region Setup Tests

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new ChangePasswordCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidPasswordTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidPassword(string password)
    {
        // Arrange
        _request.Password = password;
        var command = new ChangePasswordCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidNewPasswordTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidNewPassword(string newPassword)
    {
        // Arrange
        _request.NewPassword = newPassword;
        var command = new ChangePasswordCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    #endregion
}