using System.ComponentModel.DataAnnotations;

namespace Back_RapportRisque.Model
{
    public class PatientDTO
    {
        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string Name { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public bool Gender { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
