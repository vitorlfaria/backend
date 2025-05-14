using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Common.HealthChecks;

/// <summary>
/// Contains unit tests for the HealthChecksExtension class.
/// Tests cover adding and using basic health checks.
/// </summary>
public class HealthChecksExtensionTests
{
    /// <summary>
    /// Tests that basic health checks are added to the service collection.
    /// </summary>
    [Fact(DisplayName = "AddBasicHealthChecks should add health checks to services")]
    public void AddBasicHealthChecks_ShouldAddHealthChecksToServices()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();

        // Act
        builder.AddBasicHealthChecks();

        // Assert
        var services = builder.Services;
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
        var builder = WebApplication.CreateBuilder();
        builder.AddBasicHealthChecks();
        var app = builder.Build();

        // Act
        app.UseBasicHealthChecks();
    }
}