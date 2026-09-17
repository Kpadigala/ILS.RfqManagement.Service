namespace ILS.RfqManagement.DomainModel
{
    /// <summary>
    /// Represents an RFQ Management administrator granted for a supplier company.
    /// </summary>
    public class AdministratorDetail
    {
        /// <summary>
        /// Gets or sets the administrator record identifier.
        /// </summary>
        /// <value>
        /// The administrator record identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the supplier company the administrator was granted for.
        /// </summary>
        /// <value>
        /// The supplier company identifier.
        /// </value>
        public string SupplierCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company/user identifier granted administrator privileges.
        /// </summary>
        /// <value>
        /// The admin company identifier.
        /// </value>
        public string AdminCompanyId { get; set; }
    }
}
