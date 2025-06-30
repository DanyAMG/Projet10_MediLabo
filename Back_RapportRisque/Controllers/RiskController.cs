

using Back_RapportRisque.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_RapportRisque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskController : ControllerBase
    {
        private readonly RiskService _service;

        public RiskController(RiskService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Practicien")]
        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRisk(int patientId)
        {
            var risk = await _service.AssessPatientRiskAsync(patientId);
            return Ok(risk);
        }
    }
}
