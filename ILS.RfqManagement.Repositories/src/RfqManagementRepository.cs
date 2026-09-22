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
            var companyIds = (Clob)JsonConvert.SerializeObject(request.AdminCompanyIds);

            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spAddAdministrators",
                new
                {
                    insuppliercompanyid = request.SupplierCompanyId,
                    incompanyids = companyIds,
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

        public IEnumerable<AssignmentRuleSummary> GetAssignmentRules(string supplierCompanyId)
        {
            using var cn = _connectionFactory.Connection;
            return cn.Query<AssignmentRuleSummary>(
                "RFQ.pkgRFQManagement.spGetAssignmentRules",
                new { insuppliercompanyid = supplierCompanyId },
                new { outrules = RefCursor.Value }).ToList();
        }

        public AssignmentRuleDetail GetAssignmentRule(string assignmentRuleId)
        {
            using var cn = _connectionFactory.Connection;
            var result = cn.QueryMultiple(
                "RFQ.pkgRFQManagement.spGetAssignmentRule",
                new { inassignmentruleid = assignmentRuleId },
                new
                {
                    outrule = RefCursor.Value,
                    outcriteria = RefCursor.Value,
                    outescalation = RefCursor.Value
                });

            var rule = result.Read<AssignmentRuleDetail>().SingleOrDefault();
            if (rule == null)
                return null;

            rule.Criteria = result.Read<AssignmentCriteriaItem>().ToList();
            rule.Escalation = MapToEscalation(result.Read<EscalationRow>().SingleOrDefault());

            return rule;
        }

        public string SaveAssignmentRule(SaveAssignmentRuleRequest request, string auditUser)
        {
            var partNumbers = (Clob)JsonConvert.SerializeObject(request.PartNumbers ?? Enumerable.Empty<string>());
            var companies = (Clob)JsonConvert.SerializeObject((request.Companies ?? Enumerable.Empty<AssignmentCriteriaItem>())
                .Select(c => new { companyId = c.CriteriaCode, companyName = c.CriteriaLabel }));
            var regions = (Clob)JsonConvert.SerializeObject((request.Regions ?? Enumerable.Empty<AssignmentCriteriaItem>())
                .Select(r => new { regionId = r.CriteriaCode, regionName = r.CriteriaLabel, countryCd = r.CountryCd }));
            var escalation = request.Escalation;

            using var cn = _connectionFactory.Connection;
            var result = cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spSaveAssignmentRule",
                new
                {
                    inassignmentruleid = request.AssignmentRuleId,
                    insuppliercompanyid = request.SupplierCompanyId,
                    inassigntocompanyid = request.AssignToCompanyId,
                    inadministratorid = request.AdministratorId,
                    inpartnumbers = partNumbers,
                    incompanies = companies,
                    inregions = regions,
                    inescalationenabled = escalation != null ? (escalation.EscalationEnabled ? 1 : 0) : (int?)null,
                    inescalationadministratorid = escalation?.EscalationAdministratorId,
                    instarttime = EscalationTimeConverter.ToMinutesSinceMidnight(escalation?.StartTime),
                    inendtime = EscalationTimeConverter.ToMinutesSinceMidnight(escalation?.EndTime),
                    intimezone = escalation?.TimeZone,
                    innotifybyemail = escalation != null ? (escalation.NotifyByEmail ? 1 : 0) : (int?)null,
                    inmon = escalation != null ? (escalation.Mon ? 1 : 0) : (int?)null,
                    intue = escalation != null ? (escalation.Tue ? 1 : 0) : (int?)null,
                    inwed = escalation != null ? (escalation.Wed ? 1 : 0) : (int?)null,
                    inthu = escalation != null ? (escalation.Thu ? 1 : 0) : (int?)null,
                    infri = escalation != null ? (escalation.Fri ? 1 : 0) : (int?)null,
                    insat = escalation != null ? (escalation.Sat ? 1 : 0) : (int?)null,
                    insun = escalation != null ? (escalation.Sun ? 1 : 0) : (int?)null,
                    inaudituser = auditUser
                },
                new { outassignmentruleid = OutputParameter.Make<string>(size: 36) })
                .Get<string>("outassignmentruleid");

            return result;
        }

        public void DeleteAssignmentRule(string assignmentRuleId, string auditUser)
        {
            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spDeleteAssignmentRule",
                new
                {
                    inassignmentruleid = assignmentRuleId,
                    inaudituser = auditUser
                },
                new { });
        }

        public IEnumerable<ManualAssignment> GetManualAssignments(string rfqId)
        {
            using var cn = _connectionFactory.Connection;
            return cn.Query<ManualAssignment>(
                "RFQ.pkgRFQManagement.spGetManualAssignments",
                new { inrfqid = rfqId },
                new { outassignments = RefCursor.Value }).ToList();
        }

        public void InsManualAssignments(InsManualAssignmentsRequest request, string auditUser)
        {
            var assignToCompanyIds = (Clob)JsonConvert.SerializeObject(request.AssignToCompanyIds ?? Enumerable.Empty<string>());

            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spInsManualAssignments",
                new
                {
                    inrfqid = request.RfqId,
                    insuppliercompanyid = request.SupplierCompanyId,
                    inassigntocompanyids = assignToCompanyIds,
                    inadministratorid = request.AdministratorId,
                    inaudituser = auditUser
                },
                new { });
        }

        public void DeleteManualAssignments(DeleteManualAssignmentsRequest request, string auditUser)
        {
            var assignToCompanyIds = (Clob)JsonConvert.SerializeObject(request.AssignToCompanyIds ?? Enumerable.Empty<string>());

            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spDeleteManualAssignments",
                new
                {
                    inrfqid = request.RfqId,
                    insuppliercompanyid = request.SupplierCompanyId,
                    inassigntocompanyids = assignToCompanyIds,
                    inaudituser = auditUser
                },
                new { });
        }

        public IEnumerable<string> GetAssignedAdministrators(string supplierCompanyId, string rfqId)
        {
            using var cn = _connectionFactory.Connection;
            return cn.Query<string>(
                "RFQ.pkgRFQManagement.spGetAssignedAdministrators",
                new { insuppliercompanyid = supplierCompanyId, inrfqid = rfqId },
                new { outassigned = RefCursor.Value }).ToList();
        }

        public void MatchNewRfqToAssignmentRules(MatchNewRfqToAssignmentRulesRequest request, string auditUser)
        {
            var partNumbers = (Clob)JsonConvert.SerializeObject(request.PartNumbers ?? Enumerable.Empty<string>());

            using var cn = _connectionFactory.Connection;
            cn.ExecuteScalar(
                "RFQ.pkgRFQManagement.spMatchNewRfqToAssignmentRules",
                new
                {
                    inrfqid = request.RfqId,
                    insuppliercompanyid = request.SupplierCompanyId,
                    inbuyercompanyid = request.BuyerCompanyId,
                    inpartnumbers = partNumbers,
                    inrfqtypecd = request.RfqTypeCd,
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
                AdminCompanyId = row.CompanyId
            }).ToList();
        }

        private static AssignmentEscalation MapToEscalation(EscalationRow row)
        {
            if (row == null)
                return null;

            return new AssignmentEscalation
            {
                EscalationEnabled = row.EscalationEnabled,
                EscalationAdministratorId = row.EscalationAdministratorId,
                StartTime = EscalationTimeConverter.ToTimeString(row.StartTime),
                EndTime = EscalationTimeConverter.ToTimeString(row.EndTime),
                TimeZone = row.TimeZone,
                NotifyByEmail = row.NotifyByEmail,
                Mon = row.Mon,
                Tue = row.Tue,
                Wed = row.Wed,
                Thu = row.Thu,
                Fri = row.Fri,
                Sat = row.Sat,
                Sun = row.Sun
            };
        }

        // Shape of the ref cursor returned by RFQ.pkgRFQManagement.spGetAdministrators
        // ("administratorid", "suppliercompanyid", "companyid").
        private class AdministratorRow
        {
            public string AdministratorId { get; set; }

            public string SupplierCompanyId { get; set; }

            public string CompanyId { get; set; }
        }

        // Shape of the outescalation ref cursor returned by RFQ.pkgRFQManagement.spGetAssignmentRule.
        // starttime/endtime are stored as minutes since midnight (0-1439), not a formatted time string.
        private class EscalationRow
        {
            public bool EscalationEnabled { get; set; }

            public string EscalationAdministratorId { get; set; }

            public int? StartTime { get; set; }

            public int? EndTime { get; set; }

            public string TimeZone { get; set; }

            public bool NotifyByEmail { get; set; }

            public bool Mon { get; set; }

            public bool Tue { get; set; }

            public bool Wed { get; set; }

            public bool Thu { get; set; }

            public bool Fri { get; set; }

            public bool Sat { get; set; }

            public bool Sun { get; set; }
        }
    }
}
