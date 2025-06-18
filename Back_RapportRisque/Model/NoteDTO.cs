using System.ComponentModel.DataAnnotations;

namespace Back_RapportRisque.Model
{
    public class NoteDTO
    {
        public string Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
