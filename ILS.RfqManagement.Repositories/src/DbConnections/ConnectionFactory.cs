using ILS.ConnectionStrings.Client;
using ILS.RfqManagement.DomainModel.Contracts.Repositories;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace ILS.RfqManagement.Repositories.DbConnections
{
    [ExcludeFromCodeCoverage] // JUSTIFICATION: not unit testable due to database access
    public class ConnectionFactory : IConnectionFactory
    {
        public class ConfigurationKeys
        {
            public const string DbEnvironment = "DbConnection:Environment";
            public const string DbInstance = "DbConnection:Instance";
        }


        private readonly string _environment;
        private readonly string _instance;
        private readonly IConnectionStringsClient _connectionStringsClient;

        public ConnectionFactory(IConfiguration configuration, IConnectionStringsClient connectionStringsClient)
        {
            _environment = configuration[ConfigurationKeys.DbEnvironment];
            _instance = configuration[ConfigurationKeys.DbInstance];
            _connectionStringsClient = connectionStringsClient;
        }

        public IDbConnection Connection => new OracleConnection(
            _connectionStringsClient.GetAsync(_environment, _instance).GetAwaiter().GetResult()
        );
    }
}
