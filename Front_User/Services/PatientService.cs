using Frontend.Model;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        
        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PatientDTO>> GetAllPatientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PatientDTO>>("https://localhost:7047/patients");
        }

        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PatientDTO>($"https://localhost:7047/patients/{id}");
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
