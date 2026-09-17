using System.Data;

namespace ILS.RfqManagement.DomainModel.Contracts.Repositories
{
    public interface IConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}
