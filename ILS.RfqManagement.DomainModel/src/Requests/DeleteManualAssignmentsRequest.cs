using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Requests
{
    /// <summary>
    /// Request to remove specific manual assignments for an RFQ and supplier company.
    /// </summary>
    public class DeleteManualAssignmentsRequest
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
        /// Gets or sets the companies to remove the manual assignment for.
        /// </summary>
        /// <value>
        /// The assign-to company identifiers.
        /// </value>
        public IEnumerable<string> AssignToCompanyIds { get; set; }
    }
}
