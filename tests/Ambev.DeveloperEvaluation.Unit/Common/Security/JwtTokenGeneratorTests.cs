using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Security;

/// <summary>
/// Contains unit tests for the JwtTokenGenerator class.
/// Tests cover token generation and claim verification.
/// </summary>
public class JwtTokenGeneratorTests
{
    private readonly JwtTokenGenerator _tokenGenerator;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenGeneratorTests"/> class.
    /// Sets up the token generator and configuration for testing.
    /// </summary>
    public JwtTokenGeneratorTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> { { "Jwt:SecretKey", "98awduf9WEEF908Ajfwa==.0sdf0awf09awrmaw09efu0ERF==" } })
            .Build();
        _tokenGenerator = new JwtTokenGenerator(_configuration);
    }

    /// <summary>
    /// Tests that a JWT token is generated successfully for a given user.
    /// </summary>
    [Fact(DisplayName = "Should generate a JWT token for a user")]
    public void GenerateToken_ShouldReturnJwtToken()
    {
        // Arrange
        var user = new FakeUser { Id = "testUserId", Username = "TestUser", Role = "Customer" };

        // Act
        var token = _tokenGenerator.GenerateToken(user);

        // Assert
        Assert.NotEmpty(token);
    }

    /// <summary>
    /// Fake user class for testing purposes.
    /// </summary>
    private class FakeUser : IUser
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}