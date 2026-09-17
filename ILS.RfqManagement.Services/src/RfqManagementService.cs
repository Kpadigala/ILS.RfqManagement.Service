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
    }
}
