using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Requests
{
    /// <summary>
    /// Request to create (AssignmentRuleId is null) or edit (AssignmentRuleId is set) an assignment rule.
    /// </summary>
    public class SaveAssignmentRuleRequest
    {
        /// <summary>
        /// Gets or sets the assignment rule identifier. Null when creating a new rule.
        /// </summary>
        /// <value>
        /// The assignment rule identifier.
        /// </value>
        public string? AssignmentRuleId { get; set; }

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
        /// Gets or sets the part number criteria.
        /// </summary>
        /// <value>
        /// The part numbers.
        /// </value>
        public IEnumerable<string> PartNumbers { get; set; }

        /// <summary>
        /// Gets or sets the company criteria.
        /// </summary>
        /// <value>
        /// The companies.
        /// </value>
        public IEnumerable<AssignmentCriteriaItem> Companies { get; set; }

        /// <summary>
        /// Gets or sets the region criteria.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public IEnumerable<AssignmentCriteriaItem> Regions { get; set; }

        /// <summary>
        /// Gets or sets the after-hours escalation settings. Null clears/omits escalation for this rule.
        /// </summary>
        /// <value>
        /// The escalation settings.
        /// </value>
        public AssignmentEscalation? Escalation { get; set; }
    }
}
