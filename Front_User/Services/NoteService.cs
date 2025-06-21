using Frontend.Model;
using System.Net.Http.Json;

namespace Frontend.Services
{
    public class NoteService
    {
        private readonly HttpClient _httpClient;

        public NoteService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthenticatedClient");
        }

        public async Task<List<NoteDTO>>GetAllNotesByPatientIdAsync(int patientId)
        {
            return await _httpClient.GetFromJsonAsync<List<NoteDTO>>($"https://localhost:7047/notes/patient/{patientId}");
        }

        public async Task<NoteDTO>GetNoteByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<NoteDTO>($"https://localhost:7047/notes/{id}");
        }

        public async Task AddNoteToPatientAsync(NoteDTO note)
        {
            var response = await _httpClient.PostAsJsonAsync("notes", note);
            response.EnsureSuccessStatusCode();
        }

        public async Task EditNoteAsync(string id, NoteDTO note)
        {
            var response = await _httpClient.PutAsJsonAsync($"notes/{id}", note);
            response.EnsureSuccessStatusCode() ;
        }
    }
}
