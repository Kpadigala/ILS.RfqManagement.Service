using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics.CodeAnalysis;
namespace ILS.RfqManagement.Endpoints.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterEndpoints(this IServiceCollection services)
        {
            return services;
        }
    }
}
