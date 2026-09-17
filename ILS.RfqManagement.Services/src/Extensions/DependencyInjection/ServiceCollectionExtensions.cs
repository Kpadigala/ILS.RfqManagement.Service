using ILS.RfqManagement.DomainModel.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ILS.RfqManagement.Services.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage] // JUSTIFICATION: not unit testable due to Core dependencies
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRfqManagementService, RfqManagementService>();
            return services;
        }
    }
}
