using MongoDB.Bson;
using MongoDB.Driver;
using Back_Notes.Model;
using Back_Notes.Data;

namespace Back_Notes.Repositories
{
    /// <summary>
    /// Repository for managing Notes entities using MongoDB
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private  readonly IMongoCollection<Note> _notes;

        // Constructor initialize the MongoDB collection for notes
        public NoteRepository(MongoDbContext context)
        {
            _notes = context.Notes;
        }

        /// <summary>
        /// Gets all notes for a given patient, ordered by creation date (newest first).
        /// </summary>
        /// <param name="patientId">The ID of the patient</param>
        /// <returns>List of notes associated with the patient</returns>
        public async Task<IEnumerable<Note>> GetAllNotesByPatientIdAsync(int patientId)
        {
            return await _notes
                .Find(note => note.PatientId == patientId)
                .SortByDescending(note => note.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single note by its unique ID.
        /// </summary>
        /// <param name="id">The note's ID (MongoDB string ObjectId)</param>
        /// <returns>The note object or null if not found</returns>
        public async Task<Note> GetNoteByIdAsync(string id)
        {
            return await _notes.Find(note => note.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserts a new note into the database.
        /// </summary>
        /// <param name="note">The note to create</param>
        /// <returns>The created note</returns>
        public async Task<Note> CreateNoteAsync(Note note)
        {
            await _notes.InsertOneAsync(note);
            return note;
        }

        /// <summary>
        /// Updates an existing note based on its ID.
        /// </summary>
        /// <param name="note">The updated note object</param>
        /// <returns>The updated note</returns>
        public async Task<Note> UpdateNoteAsync(Note note)
        {
            var filter = Builders<Note>.Filter.Eq(n => n.Id, note.Id);
            await _notes.ReplaceOneAsync(filter, note);
            return note;
        }

        /// <summary>
        /// Deletes a note by its ID.
        /// </summary>
        /// <param name="id">The note's ID</param>
        /// <returns>The deleted note, or null if it didn't exist</returns>
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
