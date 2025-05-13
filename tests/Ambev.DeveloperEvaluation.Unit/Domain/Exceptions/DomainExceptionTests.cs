using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Exceptions;

/// <summary>
/// Contains unit tests for the DomainException class.
/// Tests cover exception creation with and without inner exceptions.
/// </summary>
public class DomainExceptionTests
{
    /// <summary>
    /// Tests that a DomainException is created with the correct message.
    /// </summary>
    [Fact(DisplayName = "DomainException should be created with the correct message")]
    public void Given_Message_When_ExceptionCreated_Then_ShouldHaveCorrectMessage()
    {
        // Arrange
        var message = "Test exception message";

        // Act
        var exception = new DomainException(message);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Null(exception.InnerException);
    }

    /// <summary>
    /// Tests that a DomainException is created with the correct message and inner exception.
    /// </summary>
    [Fact(DisplayName = "DomainException should be created with the correct message and inner exception")]
    public void Given_MessageAndInnerException_When_ExceptionCreated_Then_ShouldHaveCorrectMessageAndInnerException()
    {
        // Arrange
        var message = "Test exception message";
        var innerException = new Exception("Inner exception message");

        // Act
        var exception = new DomainException(message, innerException);

        // Assert
        Assert.Equal(message, exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    // Additional tests can be added to cover other scenarios or properties if needed.
}