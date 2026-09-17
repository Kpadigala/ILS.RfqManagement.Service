using ILS.Extensions;
using ILS.RfqManagement.DomainModel;
using ILS.RfqManagement.DomainModel.Contracts.Services;
using ILS.RfqManagement.DomainModel.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ILS.RfqManagement.Endpoints.Controllers
{
    /// <summary>
    /// Manages RFQ Management administrators for a supplier company.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RfqManagementController : ControllerBase
    {
        private readonly IRfqManagementService _service;

        public RfqManagementController(IRfqManagementService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves the active RFQ Management administrators for a supplier company.
        /// </summary>
        /// <param name="supplierCompanyId">The supplier company identifier.</param>
        /// <returns>The administrators granted for the specified supplier company.</returns>
        /// <response code="200">Returns the administrators.</response>
        /// <response code="400">If the supplierCompanyId is missing or invalid.</response>
        [HttpGet("administrators/{supplierCompanyId}")]
        [ProducesResponseType(typeof(IEnumerable<AdministratorDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<AdministratorDetail>> GetAdministrators(string supplierCompanyId)
        {
            if (string.IsNullOrEmpty(supplierCompanyId))
                return BadRequest(new { Message = "supplierCompanyId is empty." });

            return _service.GetAdministrators(supplierCompanyId).ToList();
        }

        /// <summary>
        /// Grants RFQ Management administrator privileges to one or more users for a supplier company.
        /// </summary>
        /// <param name="request">The supplier company and admin company ids to grant.</param>
        /// <returns>The resulting current list of administrators for the supplier company.</returns>
        /// <response code="200">Returns the resulting administrators.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost("administrators/add")]
        [ProducesResponseType(typeof(IEnumerable<AdministratorDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<AdministratorDetail>> AddAdministrators([FromBody] AddAdministratorsRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.SupplierCompanyId))
                return BadRequest(new { Message = "SupplierCompanyId is empty." });

            if (request.AdminCompanyIds == null)
                return BadRequest(new { Message = "AdminCompanyIds is empty." });

            var auditUser = HttpContext.GetUserId();

            return _service.AddAdministrators(request, auditUser).ToList();
        }

        /// <summary>
        /// Removes one or more RFQ Management administrator records.
        /// </summary>
        /// <param name="request">The administrator ids to remove.</param>
        /// <returns>True when the removal completed.</returns>
        /// <response code="200">Returns true.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost("administrators/remove")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> RemoveAdministrators([FromBody] RemoveAdministratorsRequest request)
        {
            if (request == null || request.AdministratorIds == null)
                return BadRequest(new { Message = "AdministratorIds is empty." });

            var auditUser = HttpContext.GetUserId();

            return _service.RemoveAdministrators(request, auditUser);
        }
    }
}
