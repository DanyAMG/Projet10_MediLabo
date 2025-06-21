using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Frontend.Services
{
    public class RiskService
    {
        private readonly HttpClient _httpClient;

        public RiskService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("AuthenticatedClient");
        }

        public async Task<string> AssessPatientRiskAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"/risk/{patientId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
