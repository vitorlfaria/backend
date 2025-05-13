using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.IoC.ModuleInitializers;

public class InfrastructureModuleInitializerTests
{
    [Fact]
    public void Initialize_ShouldRegisterDbContextAndRepositories()
    {
        // Arrange
        var services = new ServiceCollection();

        var initializer = new InfrastructureModuleInitializer();

        // Act
        initializer.Initialize(services);

        // Assert
        Assert.Contains(services, service => service.ServiceType == typeof(DbContext) && service.Lifetime == ServiceLifetime.Scoped);
        Assert.Contains(services, service => service.ServiceType == typeof(IUserRepository) && service.ImplementationType == typeof(UserRepository) && service.Lifetime == ServiceLifetime.Scoped);
    }
}