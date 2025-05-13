using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC;

public static class DependencyResolver
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        new ApplicationModuleInitializer().Initialize(services);
        new InfrastructureModuleInitializer().Initialize(services);
        new WebApiModuleInitializer().Initialize(services);
    }
}