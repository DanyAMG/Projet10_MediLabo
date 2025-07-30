

using Back_RapportRisque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_RapportRisque.Controllers
{
    /// <summary>
    /// Controller for assessing patient risk levels.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RiskController : ControllerBase
    {
        private readonly RiskService _service;

        
        // Constructor that injects the RiskService.
        public RiskController(RiskService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the risk level of a patient based on their ID.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>The assessed risk level as a string.</returns>
        [Authorize(Roles = "Practicien")]
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRisk(int patientId)
        {
            var risk = await _service.AssessPatientRiskAsync(patientId);
            return Ok(risk);
        }
    }
}
