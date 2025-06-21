using Back_Patient.Model;
using Back_Patient.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Back_Patient.Controllers
{
    
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
        }

        //Get: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _repository.GetAllPatientsAsync();
            if (patients == null)
            {
                return NotFound();
            }

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _repository.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }
        [Authorize(Roles = "Operateur")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            var existingPatient = await _repository.GetPatientByIdAsync(id);

            if (existingPatient == null)
            {
                return NotFound();
            }

            existingPatient.FirstName = patient.FirstName;
            existingPatient.Name = patient.Name;
            existingPatient.Adress = patient.Adress;
            existingPatient.Birthday = patient.Birthday;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.Gender = patient.Gender;

            await _repository.UpdatePatientAsync(existingPatient);
            return Ok(existingPatient);
        }
        [Authorize(Roles = "Operateur")]
        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody]Patient patientDTO)
        {
            var patient = new Patient
            {
                FirstName = patientDTO.FirstName,
                Name = patientDTO.Name,
                Adress = patientDTO.Adress,
                Birthday = patientDTO.Birthday,
                PhoneNumber = patientDTO.PhoneNumber,
                Gender =patientDTO.Gender,
            };

            var createdPatient = await _repository.CreatePatientAsync(patient);
            return Ok();
        }
        [Authorize(Roles = "Operateur")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _repository.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            await _repository.DeletePatientAsync(id);
            return Ok();
        }
    }
}
