using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC;

public interface IModuleInitializer
{
    void Initialize(IServiceCollection builder);
}
