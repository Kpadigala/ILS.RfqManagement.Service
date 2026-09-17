namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents one match criterion (part number, company, or region) for an assignment rule.
    /// </summary>
    public class AssignmentCriteriaItem
    {
        /// <summary>
        /// Gets or sets the criteria type: PART, COMPANY, or REGION.
        /// </summary>
        /// <value>
        /// The criteria type.
        /// </value>
        public string CriteriaType { get; set; }

        /// <summary>
        /// Gets or sets the criteria code (partnumber, companyid, or regionid).
        /// </summary>
        /// <value>
        /// The criteria code.
        /// </value>
        public string CriteriaCode { get; set; }

        /// <summary>
        /// Gets or sets the display label (companyname or regionname); null for PART.
        /// </summary>
        /// <value>
        /// The criteria label.
        /// </value>
        public string CriteriaLabel { get; set; }

        /// <summary>
        /// Gets or sets the country code; only populated for REGION criteria.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCd { get; set; }
    }
}
