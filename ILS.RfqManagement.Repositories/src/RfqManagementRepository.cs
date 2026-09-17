using ILS.Dapper;
using ILS.Dapper.Types;
using ILS.RfqManagement.DomainModel;
using ILS.RfqManagement.DomainModel.Contracts.Repositories;
using ILS.RfqManagement.DomainModel.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ILS.RfqManagement.Repositories
{
    // Excluded because the class is only partially unit testable
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class RfqManagementRepository : IRfqManagementRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public RfqManagementRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<AdministratorDetail> GetAdministrators(string supplierCompanyId)
        {
            using var cn = _connectionFactory.Connection;
            var rows = cn.Query<AdministratorRow>(
                "RFQ.pkgRFQManagement.spGetAdministrators",
                new { insuppliercompanyid = supplierCompanyId },
                new { outadministrators = RefCursor.Value });

            return MapToDetails(rows);
        }

        public void AddAdministrators(AddAdministratorsRequest request, string auditUser)
        {
            var userIds = (Clob)JsonConvert.SerializeObject(request.AdminCompanyIds);

            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spAddAdministrators",
                new
                {
                    insuppliercompanyid = request.SupplierCompanyId,
                    inuserids = userIds,
                    inaudituser = auditUser
                },
                new { });
        }

        public void RemoveAdministrators(RemoveAdministratorsRequest request, string auditUser)
        {
            var administratorIds = (Clob)JsonConvert.SerializeObject(request.AdministratorIds);

            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spRemoveAdministrators",
                new
                {
                    inadministratorids = administratorIds,
                    inaudituser = auditUser
                },
                new { });
        }

        private static IEnumerable<AdministratorDetail> MapToDetails(IEnumerable<AdministratorRow> rows)
        {
            return rows.Select(row => new AdministratorDetail
            {
                Id = row.AdministratorId,
                SupplierCompanyId = row.SupplierCompanyId,
                AdminCompanyId = row.UserId
            }).ToList();
        }

        // Shape of the ref cursor returned by RFQ.pkgRFQManagement.spGetAdministrators
        // ("administratorid", "suppliercompanyid", "userid").
        private class AdministratorRow
        {
            public string AdministratorId { get; set; }

            public string SupplierCompanyId { get; set; }

            public string UserId { get; set; }
        }
    }
}
