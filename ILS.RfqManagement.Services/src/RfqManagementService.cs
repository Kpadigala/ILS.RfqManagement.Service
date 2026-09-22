using ILS.RfqManagement.DomainModel;
using ILS.RfqManagement.DomainModel.Contracts.Repositories;
using ILS.RfqManagement.DomainModel.Contracts.Services;
using ILS.RfqManagement.DomainModel.Requests;
using System.Collections.Generic;

namespace ILS.RfqManagement.Services
{
    public class RfqManagementService : IRfqManagementService
    {
        private readonly IRfqManagementRepository _repository;

        public RfqManagementService(IRfqManagementRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AdministratorDetail> GetAdministrators(string supplierCompanyId)
        {
            return _repository.GetAdministrators(supplierCompanyId);
        }

        public IEnumerable<AdministratorDetail> AddAdministrators(AddAdministratorsRequest request, string auditUser)
        {
            _repository.AddAdministrators(request, auditUser);
            return _repository.GetAdministrators(request.SupplierCompanyId);
        }

        public bool RemoveAdministrators(RemoveAdministratorsRequest request, string auditUser)
        {
            _repository.RemoveAdministrators(request, auditUser);
            return true;
        }

        public IEnumerable<AssignmentRuleSummary> GetAssignmentRules(string supplierCompanyId)
        {
            return _repository.GetAssignmentRules(supplierCompanyId);
        }

        public AssignmentRuleDetail GetAssignmentRule(string assignmentRuleId)
        {
            return _repository.GetAssignmentRule(assignmentRuleId);
        }

        public AssignmentRuleDetail SaveAssignmentRule(SaveAssignmentRuleRequest request, string auditUser)
        {
            var assignmentRuleId = _repository.SaveAssignmentRule(request, auditUser);
            return _repository.GetAssignmentRule(assignmentRuleId);
        }

        public bool DeleteAssignmentRule(string assignmentRuleId, string auditUser)
        {
            _repository.DeleteAssignmentRule(assignmentRuleId, auditUser);
            return true;
        }

        public IEnumerable<ManualAssignment> GetManualAssignments(string rfqId)
        {
            return _repository.GetManualAssignments(rfqId);
        }

        public IEnumerable<ManualAssignment> InsManualAssignments(InsManualAssignmentsRequest request, string auditUser)
        {
            _repository.InsManualAssignments(request, auditUser);
            return _repository.GetManualAssignments(request.RfqId);
        }

        public IEnumerable<ManualAssignment> DeleteManualAssignments(DeleteManualAssignmentsRequest request, string auditUser)
        {
            _repository.DeleteManualAssignments(request, auditUser);
            return _repository.GetManualAssignments(request.RfqId);
        }

        public IEnumerable<string> GetAssignedAdministrators(string supplierCompanyId, string rfqId)
        {
            return _repository.GetAssignedAdministrators(supplierCompanyId, rfqId);
        }

        public bool MatchNewRfqToAssignmentRules(MatchNewRfqToAssignmentRulesRequest request, string auditUser)
        {
            _repository.MatchNewRfqToAssignmentRules(request, auditUser);
            return true;
        }

        public IEnumerable<PendingRfqMatch> GetPendingRfqMatches()
        {
            return _repository.GetPendingRfqMatches();
        }

        public bool ClearPendingRfqMatch(string rfqId)
        {
            _repository.ClearPendingRfqMatch(rfqId);
            return true;
        }
    }
}
