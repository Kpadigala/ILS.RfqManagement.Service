using ILS.RfqManagement.DomainModel.Requests;
using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Contracts.Services
{
    public interface IRfqManagementService
    {
        IEnumerable<AdministratorDetail> GetAdministrators(string supplierCompanyId);

        IEnumerable<AdministratorDetail> AddAdministrators(AddAdministratorsRequest request, string auditUser);

        bool RemoveAdministrators(RemoveAdministratorsRequest request, string auditUser);

        IEnumerable<AssignmentRuleSummary> GetAssignmentRules(string supplierCompanyId);

        AssignmentRuleDetail GetAssignmentRule(string assignmentRuleId);

        AssignmentRuleDetail SaveAssignmentRule(SaveAssignmentRuleRequest request, string auditUser);

        bool DeleteAssignmentRule(string assignmentRuleId, string auditUser);
    }
}
