

using Back_RapportRisque.Services;
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

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRisk(int patientId)
        {
            var risk = await _service.AssessPatientRiskAsync(patientId);
            return Ok(risk);
        }
    }
}
