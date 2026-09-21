using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Requests
{
    /// <summary>
    /// Request to replace the manual assignments for an RFQ and supplier company with the supplied set of companies.
    /// </summary>
    public class InsManualAssignmentsRequest
    {
        /// <summary>
        /// Gets or sets the RFQ identifier.
        /// </summary>
        /// <value>
        /// The RFQ identifier.
        /// </value>
        public string RfqId { get; set; }

        /// <summary>
        /// Gets or sets the RFQ's recipient supplier company (Receiving ID).
        /// </summary>
        /// <value>
        /// The supplier company identifier.
        /// </value>
        public string SupplierCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the companies to manually assign the RFQ to.
        /// </summary>
        /// <value>
        /// The assign-to company identifiers.
        /// </value>
        public IEnumerable<string> AssignToCompanyIds { get; set; }

        /// <summary>
        /// Gets or sets who is making this assignment.
        /// </summary>
        /// <value>
        /// The administrator identifier.
        /// </value>
        public string AdministratorId { get; set; }
    }
}
