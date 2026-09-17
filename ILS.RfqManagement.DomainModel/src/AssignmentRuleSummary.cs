namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents an assignment rule row for the Assignment tab's rule list grid.
    /// </summary>
    public class AssignmentRuleSummary
    {
        /// <summary>
        /// Gets or sets the assignment rule identifier.
        /// </summary>
        /// <value>
        /// The assignment rule identifier.
        /// </value>
        public string AssignmentRuleId { get; set; }

        /// <summary>
        /// Gets or sets the supplier company (Receiving ID) that owns this rule.
        /// </summary>
        /// <value>
        /// The supplier company identifier.
        /// </value>
        public string SupplierCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company (Assign to ID) that matching RFQs are assigned to.
        /// </summary>
        /// <value>
        /// The assign-to company identifier.
        /// </value>
        public string AssignToCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the administrator who owns this rule.
        /// </summary>
        /// <value>
        /// The administrator identifier.
        /// </value>
        public string AdministratorId { get; set; }

        /// <summary>
        /// Gets or sets a comma-separated list of the rule's part number criteria.
        /// </summary>
        /// <value>
        /// The part numbers.
        /// </value>
        public string PartNumbers { get; set; }

        /// <summary>
        /// Gets or sets a comma-separated list of the rule's company criteria ids.
        /// </summary>
        /// <value>
        /// The company ids.
        /// </value>
        public string CompanyIds { get; set; }

        /// <summary>
        /// Gets or sets a comma-separated list of the rule's company criteria names.
        /// </summary>
        /// <value>
        /// The company names.
        /// </value>
        public string CompanyNames { get; set; }

        /// <summary>
        /// Gets or sets a comma-separated list of the rule's region criteria names.
        /// </summary>
        /// <value>
        /// The region names.
        /// </value>
        public string RegionNames { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether after-hours escalation is enabled for this rule.
        /// </summary>
        /// <value>
        /// True if escalation is enabled.
        /// </value>
        public bool EscalationEnabled { get; set; }
    }
}
