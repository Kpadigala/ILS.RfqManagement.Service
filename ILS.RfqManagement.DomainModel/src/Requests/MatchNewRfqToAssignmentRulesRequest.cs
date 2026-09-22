using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Requests
{
    /// <summary>
    /// Request to match a newly-created RFQ against active assignment rules for its recipient supplier company.
    /// </summary>
    public class MatchNewRfqToAssignmentRulesRequest
    {
        /// <summary>
        /// Gets or sets the new RFQ's identifier.
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
        /// Gets or sets the buyer company that created the RFQ.
        /// </summary>
        /// <value>
        /// The buyer company identifier.
        /// </value>
        public string BuyerCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the RFQ's part numbers (including alternates).
        /// </summary>
        /// <value>
        /// The part numbers.
        /// </value>
        public IEnumerable<string> PartNumbers { get; set; }

        /// <summary>
        /// Gets or sets the RFQ type code.
        /// </summary>
        /// <value>
        /// The RFQ type code.
        /// </value>
        public string RfqTypeCd { get; set; }
    }
}
