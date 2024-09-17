using Basket.Service.Features.Commands.BasketCommands.CheckoutBasket;

namespace Basket.Service.Tests.Features.Commands.BasketCommands.CheckoutBasket;

[TestFixture]
public class CheckoutBasketValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _validator = new CheckoutBasketValidator();
        _request = new CheckoutBasketRequest
        {
            FullName = "Nguyen Van A",
            ShippingAddress = "Ho Chi Minh City",
            PhoneNumber = "0123456789"
        };
    }

    private CheckoutBasketRequest _request;
    private CheckoutBasketValidator _validator;

    private static IEnumerable<TestCaseData> InvalidRecipientNameTestCases()
    {
        yield return new TestCaseData(null).SetName("RecipientName is null");
        yield return new TestCaseData(string.Empty).SetName("RecipientName is empty");
        yield return new TestCaseData(" ").SetName("RecipientName is whitespace");
    }

    private static IEnumerable<TestCaseData> InvalidShippingAddressTestCases()
    {
        yield return new TestCaseData(null).SetName("ShippingAddress is null");
        yield return new TestCaseData(string.Empty).SetName("ShippingAddress is empty");
        yield return new TestCaseData(" ").SetName("ShippingAddress is whitespace");
    }

    private static IEnumerable<TestCaseData> InvalidRecipientPhoneTestCases()
    {
        yield return new TestCaseData(null).SetName("RecipientPhone is null");
        yield return new TestCaseData(string.Empty).SetName("RecipientPhone is empty");
        yield return new TestCaseData(" ").SetName("RecipientPhone is whitespace");
        yield return new TestCaseData("abc").SetName("RecipientPhone is not a number");
        yield return new TestCaseData("0123456789012345").SetName("RecipientPhone is greater than 15");
    }

    [Test]
    public async Task Validate_ShouldBeValid_WhenGivenValidRequest()
    {
        // Arrange
        var command = new CheckoutBasketCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.True);
    }

    [TestCaseSource(nameof(InvalidRecipientNameTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRecipientName(string recipientName)
    {
        // Arrange
        _request.FullName = recipientName;
        var command = new CheckoutBasketCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidShippingAddressTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidShippingAddress(string shippingAddress)
    {
        // Arrange
        _request.ShippingAddress = shippingAddress;
        var command = new CheckoutBasketCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }

    [TestCaseSource(nameof(InvalidRecipientPhoneTestCases))]
    public async Task Validate_ShouldBeInvalid_WhenGivenInvalidRecipientPhone(string recipientPhone)
    {
        // Arrange
        _request.PhoneNumber = recipientPhone;
        var command = new CheckoutBasketCommand(_request);

        // Act
        var actual = await _validator.ValidateAsync(command);

        // Assert
        Assert.That(actual.IsValid, Is.False);
    }
}