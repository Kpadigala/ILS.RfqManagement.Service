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

        /// <summary>
        /// Gets or sets the company id of the after-hours escalation administrator, resolved from
        /// rfq.tbrfqadministrator. Null when escalation is not enabled.
        /// </summary>
        /// <value>
        /// The escalation administrator's company identifier.
        /// </value>
        public string EscalationAdministratorCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the after-hours window start, in minutes since midnight (0-1439).
        /// </summary>
        /// <value>
        /// The escalation start time.
        /// </value>
        public int? EscalationStartMinutes { get; set; }

        /// <summary>
        /// Gets or sets the after-hours window end, in minutes since midnight (0-1439).
        /// </summary>
        /// <value>
        /// The escalation end time.
        /// </value>
        public int? EscalationEndMinutes { get; set; }

        /// <summary>
        /// Gets or sets the time zone the escalation window is evaluated in.
        /// </summary>
        /// <value>
        /// The escalation time zone.
        /// </value>
        public string EscalationTimeZone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Monday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Monday.
        /// </value>
        public bool EscalationMon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Tuesday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Tuesday.
        /// </value>
        public bool EscalationTue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Wednesday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Wednesday.
        /// </value>
        public bool EscalationWed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Thursday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Thursday.
        /// </value>
        public bool EscalationThu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Friday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Friday.
        /// </value>
        public bool EscalationFri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Saturday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Saturday.
        /// </value>
        public bool EscalationSat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether escalation applies on Sunday.
        /// </summary>
        /// <value>
        /// True if escalation applies on Sunday.
        /// </value>
        public bool EscalationSun { get; set; }
    }
}
