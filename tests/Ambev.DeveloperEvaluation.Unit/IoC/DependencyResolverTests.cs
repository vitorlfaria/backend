using Ambev.DeveloperEvaluation.IoC;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.IoC;

public class DependencyResolverTests
{
    [Fact]
    public void RegisterDependencies_ShouldRegisterAllDependencies()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.RegisterDependencies();

        // Assert
        Assert.NotEmpty(services);
    }
}
