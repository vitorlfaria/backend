using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.Security;

/// <summary>
/// Contains unit tests for the AuthenticationExtension class.
/// Tests cover JWT authentication configuration.
/// </summary>
public class AuthenticationExtensionTests
{
    /// <summary>
    /// Tests that JWT authentication is correctly configured with a valid secret key.
    /// </summary>
    [Fact(DisplayName = "Should configure JWT authentication with valid secret key")]
    public void AddJwtAuthentication_WithValidSecretKey_ShouldConfigureJwtBearer()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string> { { "Jwt:SecretKey", "validSecretKey" } })
            .Build();

        // Act
        services.AddJwtAuthentication(configuration);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var authenticationOptions = serviceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtBearerOptions>>().Value;

        Assert.True(authenticationOptions.RequireHttpsMetadata);
        Assert.True(authenticationOptions.SaveToken);
        Assert.NotNull(authenticationOptions.TokenValidationParameters);
        Assert.False(authenticationOptions.TokenValidationParameters.ValidateIssuerSigningKey);
        Assert.True(authenticationOptions.TokenValidationParameters.ValidateIssuer);
        Assert.True(authenticationOptions.TokenValidationParameters.ValidateAudience);
        Assert.Equal(TimeSpan.FromMinutes(5), authenticationOptions.TokenValidationParameters.ClockSkew);
    }

    /// <summary>
    /// Tests that an exception is thrown when the JWT secret key is missing in the configuration.
    /// </summary>
    [Fact(DisplayName = "Should throw exception when JWT secret key is missing")]
    public void AddJwtAuthentication_WithMissingSecretKey_ShouldThrowArgumentException()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build(); // Empty configuration

        // Act
        Action act = () => services.AddJwtAuthentication(configuration);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }
}