using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class WebApiModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHealthChecks();
        }
    }
}
