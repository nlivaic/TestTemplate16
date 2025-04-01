using Microsoft.Extensions.DependencyInjection;
using TestTemplate16.Application.Pipelines;

namespace TestTemplate16.Application;

public static class ServiceCollectionExtensions
{
    public static void AddTestTemplate16ApplicationHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));
        services.AddPipelines();
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }
}
