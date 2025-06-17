using Back_Notes.Repositories;
using Back_Notes.Model;
using Microsoft.AspNetCore.Mvc;

namespace Back_Notes.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _repository;

        public NoteController(INoteRepository repository)
        {
            _repository = repository;
        }

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
