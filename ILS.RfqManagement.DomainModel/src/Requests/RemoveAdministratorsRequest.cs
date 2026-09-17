using System.Collections.Generic;

namespace ILS.RfqManagement.DomainModel.Requests
{
    /// <summary>
    /// Request to remove one or more RFQ Management administrator records.
    /// </summary>
    public class RemoveAdministratorsRequest
    {
        /// <summary>
        /// Gets or sets the administrator record identifiers to remove.
        /// </summary>
        /// <value>
        /// The administrator identifiers.
        /// </value>
        public IEnumerable<string> AdministratorIds { get; set; }
    }
}
