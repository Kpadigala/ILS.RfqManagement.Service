using ILS.RfqManagement.DomainModel.Requests;
using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Contracts.Repositories
{
    public interface IRfqManagementRepository
    {
        IEnumerable<AdministratorDetail> GetAdministrators(string supplierCompanyId);

        void AddAdministrators(AddAdministratorsRequest request, string auditUser);

        void RemoveAdministrators(RemoveAdministratorsRequest request, string auditUser);

        IEnumerable<AssignmentRuleSummary> GetAssignmentRules(string supplierCompanyId);

        AssignmentRuleDetail GetAssignmentRule(string assignmentRuleId);

        string SaveAssignmentRule(SaveAssignmentRuleRequest request, string auditUser);

        void DeleteAssignmentRule(string assignmentRuleId, string auditUser);

        IEnumerable<ManualAssignment> GetManualAssignments(string rfqId);

        void InsManualAssignments(InsManualAssignmentsRequest request, string auditUser);

        void DeleteManualAssignments(DeleteManualAssignmentsRequest request, string auditUser);

        IEnumerable<string> GetAssignedAdministrators(string supplierCompanyId, string rfqId);

        void MatchNewRfqToAssignmentRules(MatchNewRfqToAssignmentRulesRequest request, string auditUser);

        IEnumerable<PendingRfqMatch> GetPendingRfqMatches();

        void ClearPendingRfqMatch(string rfqId);
    }
}
