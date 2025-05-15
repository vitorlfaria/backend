using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Unit.Fixtures;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.HealthChecks;

/// <summary>
/// Contains unit tests for the HealthChecksExtension class.
/// Tests cover adding and using basic health checks.
/// </summary>
public class HealthChecksExtensionTests : IClassFixture<WebApplicationFixture>
{
    private readonly WebApplicationFixture _fixture;

    public HealthChecksExtensionTests(WebApplicationFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Tests that basic health checks are added to the service collection.
    /// </summary>
    [Fact(DisplayName = "AddBasicHealthChecks should add health checks to services")]
    public void AddBasicHealthChecks_ShouldAddHealthChecksToServices()
    {
        // Act
        _fixture.Builder.AddBasicHealthChecks();

        // Assert
        var services = _fixture.Builder.Services;
        var healthCheckServiceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(HealthCheckService));
        Assert.NotNull(healthCheckServiceDescriptor);
        Assert.Equal(ServiceLifetime.Singleton, healthCheckServiceDescriptor.Lifetime);
    }

    /// <summary>
    /// Tests that basic health checks are added to WebApplication.
    /// </summary>
    [Fact(DisplayName = "AddBasicHealthChecks should add health checks to services")]
    public void AddBasicHealthChecks_ShouldAddHealthChecksToWebApplication()
    {
        // Arrange
        WebApplication app = _fixture.CreateWebApplication();

        // Act
        app.UseBasicHealthChecks();
    }
}