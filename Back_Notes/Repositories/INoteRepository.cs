using Back_Notes.Model;

namespace Back_Notes.Repositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesByPatientIdAsync(int patientId);
        Task<Note> GetNoteByIdAsync(string id);
        Task<Note> CreateNoteAsync(Note note);
        Task<Note> UpdateNoteAsync(Note note);
        Task<Note> DeleteNoteAsync(string id);
    }
}
