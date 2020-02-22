using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotesApp.Controllers.Resources;
using NotesApp.Helpers;
using NotesApp.Models;
using NotesApp.Services;
using System;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INoteService noteService, ILogger<NotesController> logger)
        {
            this._noteService = noteService;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] NoteQuery query)
        {
            try
            {
                var notes = await _noteService.GetAll(query, User.Identity.IsAuthenticated, User.Identity.Name);
                _logger.LogInformation("Notes successfully retrieved");
                return Ok(notes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all notes. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var note = await _noteService.GetById(id, User.Identity.IsAuthenticated, User.Identity.Name);

                if (note == null)
                {
                    _logger.LogError($"Note with id {id} not found");
                    return NotFound();
                }

                _logger.LogInformation($"Note {note.Name} successfully retrieved");
                return Ok(note);
            }
            catch(UnauthorizedAccessException)
            {
                _logger.LogError("User cannot access this note");
                return Unauthorized("User cannot access this note");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Note model)
        {
            try
            {
                var note = await _noteService.Create(model, User.Identity.Name);
                _logger.LogInformation($"Note {note.Name} successfully created");
                return Ok(note);

            }catch (UnauthorizedAccessException)
            {
                _logger.LogInformation("User cannot create note connected to other user");
                return Unauthorized("User cannot create note connected to other user");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Note model)
        {
            try
            {
                var updatedNote = await _noteService.Update(id, model, User.Identity.Name);
                _logger.LogInformation($"Note {updatedNote.Name} successfully updated.");
                return Ok(updatedNote);
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogError("User cannot update note of another user");
                return Unauthorized("User cannot update note of another user");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _noteService.Delete(id,User.Identity.Name);
                _logger.LogInformation("Note successfully deleted.");
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogInformation("User cannot delete note of another user");
                return Unauthorized("User cannot delete note of another user");
            }
            catch (DomainException ex) {
                _logger.LogError("Note not found");
                return NotFound(ex);
            }
        }
    }
}
