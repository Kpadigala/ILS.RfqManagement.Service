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

        /// <summary>
        /// Retrieves the active assignment rules for a supplier company.
        /// </summary>
        /// <param name="supplierCompanyId">The supplier company identifier.</param>
        /// <returns>The assignment rules for the specified supplier company.</returns>
        /// <response code="200">Returns the assignment rules.</response>
        /// <response code="400">If the supplierCompanyId is missing or invalid.</response>
        [HttpGet("assignments/{supplierCompanyId}")]
        [ProducesResponseType(typeof(IEnumerable<AssignmentRuleSummary>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<AssignmentRuleSummary>> GetAssignmentRules(string supplierCompanyId)
        {
            if (string.IsNullOrEmpty(supplierCompanyId))
                return BadRequest(new { Message = "supplierCompanyId is empty." });

            return _service.GetAssignmentRules(supplierCompanyId).ToList();
        }

        /// <summary>
        /// Retrieves a single assignment rule, including its criteria and escalation settings.
        /// </summary>
        /// <param name="assignmentRuleId">The assignment rule identifier.</param>
        /// <returns>The assignment rule detail.</returns>
        /// <response code="200">Returns the assignment rule.</response>
        /// <response code="400">If the assignmentRuleId is missing or invalid.</response>
        /// <response code="404">If no rule exists with the specified id.</response>
        [HttpGet("assignments/rule/{assignmentRuleId}")]
        [ProducesResponseType(typeof(AssignmentRuleDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AssignmentRuleDetail> GetAssignmentRule(string assignmentRuleId)
        {
            if (string.IsNullOrEmpty(assignmentRuleId))
                return BadRequest(new { Message = "assignmentRuleId is empty." });

            var rule = _service.GetAssignmentRule(assignmentRuleId);

            if (rule == null)
                return NotFound();

            return rule;
        }

        /// <summary>
        /// Creates a new assignment rule, or edits an existing one when AssignmentRuleId is set.
        /// </summary>
        /// <param name="request">The assignment rule to save.</param>
        /// <returns>The saved assignment rule.</returns>
        /// <response code="200">Returns the saved assignment rule.</response>
        /// <response code="400">If the request is invalid.</response>
        [HttpPost("assignments/save")]
        [ProducesResponseType(typeof(AssignmentRuleDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AssignmentRuleDetail> SaveAssignmentRule([FromBody] SaveAssignmentRuleRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.SupplierCompanyId))
                return BadRequest(new { Message = "SupplierCompanyId is empty." });

            if (string.IsNullOrEmpty(request.AssignToCompanyId))
                return BadRequest(new { Message = "AssignToCompanyId is empty." });

            if (string.IsNullOrEmpty(request.AdministratorId))
                return BadRequest(new { Message = "AdministratorId is empty." });

            var auditUser = HttpContext.GetUserId();

            return _service.SaveAssignmentRule(request, auditUser);
        }

        /// <summary>
        /// Deletes an assignment rule.
        /// </summary>
        /// <param name="assignmentRuleId">The assignment rule identifier.</param>
        /// <returns>True when the deletion completed.</returns>
        /// <response code="200">Returns true.</response>
        /// <response code="400">If the assignmentRuleId is missing or invalid.</response>
        [HttpDelete("assignments/{assignmentRuleId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> DeleteAssignmentRule(string assignmentRuleId)
        {
            if (string.IsNullOrEmpty(assignmentRuleId))
                return BadRequest(new { Message = "assignmentRuleId is empty." });

            var auditUser = HttpContext.GetUserId();

            return _service.DeleteAssignmentRule(assignmentRuleId, auditUser);
        }
    }
}
