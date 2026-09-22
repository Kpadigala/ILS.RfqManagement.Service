namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents one RFQ/supplier pair still awaiting assignment-rule matching. Polled by the RFQ Assignment
    /// matching batch application.
    /// </summary>
    public class PendingRfqMatch
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
        /// Gets or sets the buyer company that created the RFQ.
        /// </summary>
        /// <value>
        /// The buyer company identifier.
        /// </value>
        public string BuyerCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the RFQ type code.
        /// </summary>
        /// <value>
        /// The RFQ type code.
        /// </value>
        public string RfqTypeCd { get; set; }

        /// <summary>
        /// Gets or sets a comma-separated list of this supplier's part numbers (including alternates) on the RFQ.
        /// </summary>
        /// <value>
        /// The part numbers.
        /// </value>
        public string PartNumbers { get; set; }
    }
}
