using Frontend.Model;
using Frontend.Pages;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthenticatedClient");
        }

        public async Task<List<PatientDTO>> GetAllPatientsAsync()
        {
            var patients = await _httpClient.GetFromJsonAsync<List<PatientDTO>>("patients");
            Console.WriteLine($"Patients récupérés : {patients?.Count}");
            return patients;
        }

        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PatientDTO>($"http://localhost:5092/patients/{id}");
        }

        public async Task AddPatientAsync(PatientDTO patient)
        {
            var response = await _httpClient.PostAsJsonAsync("patients", patient);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditPatientAsync(int id, PatientDTO patient)
        {
            var response = await _httpClient.PutAsJsonAsync($"patients/{id}", patient);
            response.EnsureSuccessStatusCode();
        }
    }
}
