using System.Net.Security;
using Contracts.Helpers;
using Contracts.MassTransit.Messages.Events;
using NUnit.Framework;

namespace Contracts.Tests.Helpers;

[TestFixture]
[TestOf(typeof(CustomKebabNameGenerator))]
public class CustomKebabNameGeneratorTest
{

    [Test]
    public void ShouldCorrect_WhenPrefix_And_Sending()
    {
        // Arrange
        var kebabNameGenerator = new CustomKebabNameGenerator();
        
        // Act
        var result = kebabNameGenerator.SantinizeSendingExchangeName("Hello.Masstransit.IAccountCreated");
        
        // Assert
        Assert.That(result, Is.EqualTo("send-account-created"));
        
    }
    
    [Test]
    public void ShouldCorrect_WhenPrefix_And_Receiving()
    {
        // Arrange
        var kebabNameGenerator = new CustomKebabNameGenerator();
        
        // Act
        var result = kebabNameGenerator.SantinizeReceivingQueueName("Hello.Masstransit.IAccountCreated");
        
        // Assert
        Assert.That(result, Is.EqualTo("account-created"));
        
    }
}