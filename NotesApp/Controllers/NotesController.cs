using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Controllers.Resources;
using NotesApp.Models;
using NotesApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;

        public NotesController(IMapper mapper, INoteService noteService)
        {
            this._mapper = mapper;
            this._noteService = noteService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var notes = _noteService.GetAll();
            var model = _mapper.Map<NoteResource>(notes);
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var note = _noteService.GetById(id, User.Identity.IsAuthenticated, User.Identity.Name);
                var model = _mapper.Map<NoteResource>(note);

                return Ok(model);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] NoteResource model)
        {
            var note = _mapper.Map<NoteResource, Note>(model);

            note = _noteService.Create(note);
            var result = _mapper.Map<Note, NoteResource>(note);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] NoteResource model)
        {
            try
            {
                var updatedNote = _noteService.Update(id, _mapper.Map<NoteResource, Note>(model), User.Identity.Name);
                var result = _mapper.Map<Note, NoteResource>(updatedNote);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _noteService.Delete(id,User.Identity.Name);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
