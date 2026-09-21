namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents one manual (non-rule-driven) assignment of an RFQ to a company.
    /// </summary>
    public class ManualAssignment
    {
        /// <summary>
        /// Gets or sets the assignment identifier.
        /// </summary>
        /// <value>
        /// The assignment identifier.
        /// </value>
        public string AssignmentId { get; set; }

        /// <summary>
        /// Gets or sets the assigned RFQ identifier.
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
        /// Gets or sets the company the RFQ is manually assigned to.
        /// </summary>
        /// <value>
        /// The assign-to company identifier.
        /// </value>
        public string AssignToCompanyId { get; set; }

        /// <summary>
        /// Gets or sets who manually made this assignment.
        /// </summary>
        /// <value>
        /// The administrator identifier.
        /// </value>
        public string AdministratorId { get; set; }
    }
}
