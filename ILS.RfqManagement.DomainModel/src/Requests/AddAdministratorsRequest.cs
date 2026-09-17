using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Requests
{
    /// <summary>
    /// Request to grant RFQ Management administrator privileges to one or more users for a supplier company.
    /// </summary>
    public class AddAdministratorsRequest
    {
        /// <summary>
        /// Gets or sets the supplier company the administrators are being granted for.
        /// </summary>
        /// <value>
        /// The supplier company identifier.
        /// </value>
        public string SupplierCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company/user identifiers to grant administrator privileges to.
        /// </summary>
        /// <value>
        /// The admin company identifiers.
        /// </value>
        public IEnumerable<string> AdminCompanyIds { get; set; }
    }
}
