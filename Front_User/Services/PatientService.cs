using Frontend.Model;
using Frontend.Pages;
using System.Net.Http.Json;

namespace Frontend.Services
{
    /// <summary>
    /// Provides methods for managing patient data via HTTP requests to the Patient API.
    /// </summary>
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the PatientService class.
        /// </summary>
        /// <param name="httpClient">The HTTP client factory used to create an authenticated client.</param>
        public PatientService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthenticatedClient");
        }

        /// <summary>
        /// Retrieves a list of all patients.
        /// </summary>
        /// <returns>A list of PatientDTO.</returns>
        public async Task<List<PatientDTO>> GetAllPatientsAsync()
        {
            var patients = await _httpClient.GetFromJsonAsync<List<PatientDTO>>("patients");
            Console.WriteLine($"Patients récupérés : {patients?.Count}");
            return patients;
        }

        /// <summary>
        /// Retrieves a specific patient by their ID.
        /// </summary>
        /// <param name="id">The ID of the patient.</param>
        /// <returns>A PatientDTO instance.</returns>
        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PatientDTO>($"http://localhost:5092/patients/{id}");
        }

        /// <summary>
        /// Sends a POST request to add a new patient.
        /// </summary>
        /// <param name="patient">The patient data to be added.</param>
        public async Task AddPatientAsync(PatientDTO patient)
        {
            var response = await _httpClient.PostAsJsonAsync("patients", patient);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Sends a PUT request to update an existing patient's information.
        /// </summary>
        /// <param name="id">The ID of the patient to update.</param>
        /// <param name="patient">The updated patient data.</param>
        public async Task EditPatientAsync(int id, PatientDTO patient)
        {
            var response = await _httpClient.PutAsJsonAsync($"patients/{id}", patient);
            response.EnsureSuccessStatusCode();
        }
    }
}
