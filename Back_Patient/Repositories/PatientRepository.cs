using Microsoft.EntityFrameworkCore;
using Back_Patient.Data;
using Back_Patient.Model;

namespace Back_Patient.Repositories
{
    /// <summary>
    /// Repository for managing Patients entities using Entity Framework Core.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly LocalDbContext _context;

        // Constructor initialize the SQL Local DB 
        public PatientRepository(LocalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all patients from the database.
        /// </summary>
        /// <returns>A list of patients.</returns>
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        /// <summary>
        /// Gets a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>The matching patient or null if not found.</returns>
        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        /// <summary>
        /// Creates a new patient in the database.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        /// <returns>The created patient.</returns>
        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
             _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        /// <summary>
        /// Updates an existing patient in the database.
        /// </summary>
        /// <param name="patient">The patient with updated information.</param>
        /// <returns>The updated patient.</returns>
        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return patient;
        }

        /// <summary>
        /// Deletes a patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient to delete.</param>
        /// <returns>The deleted patient.</returns>
        /// <exception cref="Exception">Thrown if the patient is not found.</exception>
        public async Task<Patient> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                 throw new Exception("Patient not found");
            }
              
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            return patient;
            
        }
    }
}
