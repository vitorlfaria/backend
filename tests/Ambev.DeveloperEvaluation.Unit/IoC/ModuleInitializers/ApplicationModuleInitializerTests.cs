using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.IoC.ModuleInitializers;

public class ApplicationModuleInitializerTests
{
    [Fact]
    public void Initialize_ShouldRegisterPasswordHasher()
    {
        // Arrange
        var services = new ServiceCollection();

        var initializer = new ApplicationModuleInitializer();

        // Act
        initializer.Initialize(services);

        // Assert
        Assert.Contains(services, service => service.ServiceType == typeof(IPasswordHasher) && service.ImplementationType == typeof(BCryptPasswordHasher) && service.Lifetime == ServiceLifetime.Singleton);
    }
}