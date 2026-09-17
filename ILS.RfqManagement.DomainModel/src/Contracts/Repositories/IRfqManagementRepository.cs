using ILS.RfqManagement.DomainModel.Requests;
using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Contracts.Repositories
{
    public interface IRfqManagementRepository
    {
        IEnumerable<AdministratorDetail> GetAdministrators(string supplierCompanyId);

        void AddAdministrators(AddAdministratorsRequest request, string auditUser);

        void RemoveAdministrators(RemoveAdministratorsRequest request, string auditUser);
    }
}
