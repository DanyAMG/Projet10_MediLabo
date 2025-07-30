using Back_RapportRisque.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;

namespace Back_RapportRisque.Services
{
    /// <summary>
    /// Service responsible for assessing the health risk level of a patient based on medical notes and patient data.
    /// </summary>
    public class RiskService 
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor that takes an HttpClient instance for making API calls.
        /// </summary>
        public RiskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Analyzes the patient's notes and personal information to determine a risk level.
        /// </summary>
        /// <param name="patientId">The ID of the patient.</param>
        /// <returns>A string indicating the patient's risk level.</returns>
        public async Task<string> AssessPatientRiskAsync(int patientId)
        {
            var patient = await _httpClient.GetFromJsonAsync<PatientDTO>($"patients/{patientId}");

            var notes = await _httpClient.GetFromJsonAsync<List<NoteDTO>>($"notes/patient/{patientId}");

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

            // Count how many unique triggers terms are present in the notes
            var triggerCount = notes.Where(n => !string.IsNullOrEmpty(n.Content))
                .SelectMany(n => triggerTerms.Where(term => n.Content.Contains(term, StringComparison.OrdinalIgnoreCase)))
                .Distinct()
                .Count();

            var age = CalculateAge(patient.Birthday);
            var gender = patient.Gender;

            // Determine risk level based on age, gender and the trigger count
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

        /// <summary>
        /// Calculates a person's age based on their birth date.
        /// </summary>
        /// <param name="birthday">The date of birth.</param>
        /// <returns>The calculated age.</returns>
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
