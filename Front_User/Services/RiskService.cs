using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Frontend.Services
{
    /// <summary>
    /// Provides functionality to assess the medical risk level of a patient.
    /// </summary>
    public class RiskService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the RiskService class using a named HTTP client.
        /// </summary>
        /// <param name="httpClient">The HTTP client factory used to create an authenticated client.</param>
        public RiskService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthenticatedClient");
        }

        /// <summary>
        /// Calls the API to assess the risk level of a given patient.
        /// </summary>
        /// <param name="patientId">The ID of the patient to assess.</param>
        /// <returns>A string representing the risk level (None, Borderline, In Danger, Early Onset).</returns>
        public async Task<string> AssessPatientRiskAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"/risk/{patientId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
