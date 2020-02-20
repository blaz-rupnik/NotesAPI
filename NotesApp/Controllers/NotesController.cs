using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;
        private readonly INoteService _noteService;

        public NotesController(IMapper mapper, INoteService noteService)
        {
            this._mapper = mapper;
            this._noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var folders = await _noteService.GetAll();
            return Ok(folders);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var note = await _noteService.GetById(id, User.Identity.IsAuthenticated, User.Identity.Name);
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
            var note = await _noteService.Create(model);
            return Ok(note);
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
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
