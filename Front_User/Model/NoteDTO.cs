using System.ComponentModel.DataAnnotations;

namespace Frontend.Model
{
    public class NoteDTO
    {
        public string Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
