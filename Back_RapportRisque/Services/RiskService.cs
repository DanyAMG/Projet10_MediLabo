using Back_RapportRisque.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Back_RapportRisque.Services
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
            var patient = await _httpClient.GetFromJsonAsync<PatientDTO>($"patients/{patientId}");
            var notes = await _httpClient.GetFromJsonAsync<List<NoteDTO>>($"https://localhost:7047/notes/patient/{patientId}");

            if (patient == null)
            {
                return "Patient Not Found";
            }

            var triggerTerms = new List<string>
            {
                "Hémoglobine A1C",
                "Microalbumine",
                "Taille",
                "Poids",
                "Fumeur",
                "Fumeuse",
                "Anormal",
                "Cholestérol",
                "Vertiges",
                "Rechute",
                "Réaction",
                "Anticorps"
            };

            var triggerCount = notes.Where(n => !string.IsNullOrEmpty(n.Content))
                .SelectMany(n => triggerTerms.Where(term => n.Content.Contains(term, StringComparison.OrdinalIgnoreCase)))
                .Distinct()
                .Count();

            var age = CalculateAge(patient.Birthday);
            var gender = patient.Gender;

            if (triggerCount == 0)
            {
                return "None";
            }

            if (triggerCount >=2 && triggerCount <=5 && age > 30)
            {
                return "Borderline";
            }

            if (age < 30)
            {
                if (gender == true)
                {
                    if (triggerCount >= 5)
                    {
                        return "Early Onset";
                    }
                    if (triggerCount >= 3)
                    {
                        return "In Danger";
                    }
                }
                else if (gender == false)
                {
                    if (triggerCount >= 7)
                    {
                        return "Early Onset";
                    }
                    if (triggerCount >= 4)
                    {
                        return "In Danger";
                    }
                }
            }
            else
            {
                if (triggerCount >= 8)
                {
                    return "Early Onset";
                }

                if (triggerCount >= 6)
                {
                    return "In Danger";
                }
            }
            return "None";
        }

        private int CalculateAge(DateTime birthday)
        {
            var today = DateTime.Now;
            var age = today.Year - birthday.Year;

            if (birthday.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
