using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Controllers.Resources;
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

        public NotesController(INoteService noteService)
        {
            this._noteService = noteService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] NoteQuery query)
        {
            var folders = await _noteService.GetAll(query, User.Identity.IsAuthenticated);
            return Ok(folders);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var note = await _noteService.GetById(id, User.Identity.IsAuthenticated, User.Identity.Name);

                if (note == null)
                    return NotFound();

                return Ok(note);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Note model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var note = await _noteService.Create(model, User.Identity.Name);
                return Ok(note);

            }catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Note model)
        {
            try
            {
                var updatedNote = await _noteService.Update(id, model, User.Identity.Name);
                return Ok(updatedNote);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _noteService.Delete(id,User.Identity.Name);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
