using MongoDB.Bson;
using MongoDB.Driver;
using Back_Notes.Model;
using Back_Notes.Data;

namespace Back_Notes.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private  readonly IMongoCollection<Note> _notes;

        public NoteRepository(MongoDbContext context)
        {
            _notes = context.Notes;
        }

        public async Task<IEnumerable<Note>> GetAllNotesByPatientIdAsync(int patientId)
        {
            return await _notes
                .Find(note => note.PatientId == patientId)
                .SortByDescending(note => note.CreatedAt)
                .ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            return await _notes.Find(note => note.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            await _notes.InsertOneAsync(note);
            return note;
        }

        public async Task<Note> UpdateNoteAsync(Note note)
        {
            var filter = Builders<Note>.Filter.Eq(n => n.Id, note.Id);
            await _notes.ReplaceOneAsync(filter, note);
            return note;
        }

        public async Task<Note> DeleteNoteAsync(string id)
        {
            var filter = Builders<Note>.Filter.Eq(n => n.Id, id);
            var note = await _notes.Find(filter).FirstOrDefaultAsync();

            if (note != null)
            {
                await _notes.DeleteOneAsync(filter);
            }

            return note;
        }
    }
}
