using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Controllers.Resources;
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

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var note = _noteService.GetById(id);
            var model = _mapper.Map<NoteResource>(note);

            return Ok(model);
        }
    }
}
