using Ambev.DeveloperEvaluation.Common.Security;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Security;

/// <summary>
/// Contains unit tests for the BCryptPasswordHasher class.
/// Tests cover password hashing and verification.
/// </summary>
public class BCryptPasswordHasherTests
{
    private readonly BCryptPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="BCryptPasswordHasherTests"/> class.
    /// Sets up the password hasher for testing.
    /// </summary>
    public BCryptPasswordHasherTests()
    {
        _passwordHasher = new BCryptPasswordHasher();
    }

    /// <summary>
    /// Tests that a password can be successfully hashed.
    /// </summary>
    [Fact(DisplayName = "Should hash a password")]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        // Arrange
        var password = "testPassword";

        // Act
        var hashedPassword = _passwordHasher.HashPassword(password);

        // Assert
        Assert.NotEmpty(hashedPassword);
        Assert.NotEqual(password, hashedPassword);
    }

    /// <summary>
    /// Tests that a password can be successfully verified against its hash.
    /// </summary>
    [Fact(DisplayName = "Should verify a password against its hash")]
    public void VerifyPassword_ShouldReturnTrueForMatchingPassword()
    {
        // Arrange
        var password = "testPassword";
        var hashedPassword = _passwordHasher.HashPassword(password);

        // Act
        var isVerified = _passwordHasher.VerifyPassword(password, hashedPassword);

        // Assert
        Assert.True(isVerified);
    }
}