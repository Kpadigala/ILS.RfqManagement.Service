using ILS.ConnectionStrings.Client;
using ILS.Dapper.Extensions.DependencyInjection;
using ILS.RfqManagement.DomainModel.Contracts.Repositories;
using ILS.RfqManagement.Repositories.DbConnections;
using ILS.RfqManagement.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ILS.RfqManagement.Repositories.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage] // JUSTIFICATION: not unit testable due to Core dependencies
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IConnectionStringsClient, ConnectionStringsClient>()
                .AddScoped<IConnectionFactory, ConnectionFactory>()
                .AddScoped<IRfqManagementRepository, RfqManagementRepository>()
                .RegisterDataTypeHandlers();
        }
    }
}
