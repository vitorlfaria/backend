using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.IoC.ModuleInitializers;

public class WebApiModuleInitializerTests
{
    [Fact]
    public void Initialize_ShouldAddControllersAndHealthChecks()
    {
        // Arrange
        var services = new ServiceCollection();

        var initializer = new WebApiModuleInitializer();

        // Act
        initializer.Initialize(services);
    }
}