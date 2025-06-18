namespace Frontend.Services
{
    public class RiskService
    {
        private readonly HttpClient _httpClient;

        public RiskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AssessPatientRiskAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"/risk/{patientId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
