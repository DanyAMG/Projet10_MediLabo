using Back_Notes.Model;
using Back_Notes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_Notes.Controllers
{
    /// <summary>
    /// Controller who's managing notes related to a patient
    /// </summary>
    [ApiController]
    [Route("api/notes")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _repository;

        // Constructor with dependency injection of the Note Repository
        public NoteController(INoteRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// Get all notes linked to a specific patient.
        /// </summary>
        /// <param name="patientId"> The patient's unique identifiant</param>
        /// <returns></returns>
        [Authorize(Roles = "Practicien")]
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesAync(int patientId)
        {
            var notes = await _repository.GetAllNotesByPatientIdAsync(patientId);
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }

        /// <summary>
        /// Creates a new note for a patient.
        /// </summary>
        /// <param name="noteDTO">The note data sent from the client</param>
        /// <returns>The created note</returns>
        [Authorize(Roles = "Practicien")]
        [HttpPost]
        public async Task<IActionResult> PostNoteAsync([FromBody]Note noteDTO)
        {
            var note = new Note
            {
                Content = noteDTO.Content,
                PatientId = noteDTO.PatientId,
                CreatedAt = DateTime.UtcNow
            };

            var createdNote = await _repository.CreateNoteAsync(note);
            return Ok(createdNote);
        }

        /// <summary>
        /// Updates the content of an existing note.
        /// </summary>
        /// <param name="id">The note's unique identifiant</param>
        /// <param name="note">The updated note object</param>
        /// <returns>The updated note or 404 if not found</returns>
        [Authorize(Roles = "Practicien")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(string id, Note note)
        {
            var existingNote = await _repository.GetNoteByIdAsync(id);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Content = note.Content;
            
            await _repository.UpdateNoteAsync(existingNote);
            return Ok(existingNote);
        }

        /// <summary>
        /// Deletes a note by its ID.
        /// </summary>
        /// <param name="id">The note's unique identifiant</param>
        /// <returns>200 OK or 404 if the note doesn't exist</returns>
        [Authorize(Roles = "Practicien")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(string id)
        {
            var note = await _repository.GetNoteByIdAsync(id);

            if (note == null)
            {
                return NotFound();   
            }

            await _repository.DeleteNoteAsync(id);
            return Ok();
        }
    }
}
