using Back_Patient.Model;
using Back_Patient.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Back_Patient.Controllers
{
    /// <summary>
    /// Controller to manage patient data
    /// </summary>
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all patients present in the database.
        /// </summary>
        /// <returns>A list of all patients.</returns>
        [Authorize(Roles = "Organisateur, Practicien")]
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

        /// <summary>
        /// Gets a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The requested patient.</returns>
        [Authorize(Roles = "Organisateur, Practicien")]
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

        /// <summary>
        /// Updates an existing patient.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="patient">The updated patient data.</param>
        /// <returns>The updated patient.</returns>
        [Authorize(Roles = "Organisateur")]
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

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="patientDTO">The patient data to create.</param>
        /// <returns>Status 200 if successful.</returns>
        [Authorize(Roles = "Organisateur")]
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

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>Status 200 if successful.</returns>
        [Authorize(Roles = "Organisateur")]
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
