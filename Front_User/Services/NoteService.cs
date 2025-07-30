using Frontend.Model;
using System.Net.Http.Json;

namespace Frontend.Services
{
    /// <summary>
    /// Provides methods for managing patient notes via HTTP requests to the Note API.
    /// </summary>
    public class NoteService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the NoteService class.
        /// </summary>
        /// <param name="httpClient">The HTTP client factory used to create an authenticated client.</param>
        public NoteService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthenticatedClient");
        }

        /// <summary>
        /// Retrieves all notes associated with a specific patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>A list of notes.</returns>
        public async Task<List<NoteDTO>>GetAllNotesByPatientIdAsync(int patientId)
        {
            return await _httpClient.GetFromJsonAsync<List<NoteDTO>>($"http://localhost:5092/notes/patient/{patientId}");
        }

        /// <summary>
        /// Retrieves a single note by its ID.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <returns>The note with the specified ID.</returns>
        public async Task<NoteDTO>GetNoteByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<NoteDTO>($"http://localhost:5092/notes/{id}");
        }

        /// <summary>
        /// Sends a POST request to add a new note for a patient.
        /// </summary>
        /// <param name="note">The note data to be added.</param>
        public async Task AddNoteToPatientAsync(NoteDTO note)
        {
            var response = await _httpClient.PostAsJsonAsync("notes", note);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Sends a PUT request to update an existing note.
        /// </summary>
        /// <param name="id">The ID of the note to update.</param>
        /// <param name="note">The updated note data.</param>
        public async Task EditNoteAsync(string id, NoteDTO note)
        {
            var response = await _httpClient.PutAsJsonAsync($"notes/{id}", note);
            response.EnsureSuccessStatusCode() ;
        }
    }
}
