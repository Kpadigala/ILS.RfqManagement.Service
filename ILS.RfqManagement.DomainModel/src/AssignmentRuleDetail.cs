using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents a full assignment rule (header, criteria, and escalation) for the Assignment rule edit form.
    /// </summary>
    public class AssignmentRuleDetail
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
        /// Gets or sets the rule's match criteria (part number, company, and region rows).
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public IEnumerable<AssignmentCriteriaItem> Criteria { get; set; }

        /// <summary>
        /// Gets or sets the rule's after-hours escalation settings, if configured.
        /// </summary>
        /// <value>
        /// The escalation settings.
        /// </value>
        public AssignmentEscalation Escalation { get; set; }
    }
}
