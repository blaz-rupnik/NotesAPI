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
    public class FoldersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFolderService _folderService;

        public FoldersController(IMapper mapper, IFolderService folderService)
        {
            this._mapper = mapper;
            this._folderService = folderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var folders = await _folderService.GetAll();
            return Ok(folders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var folder = await _folderService.GetById(id, User.Identity.Name);
                return Ok(folder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Folder model)
        {
            var folder = await _folderService.Create(model);
            return Ok(folder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Folder model)
        {
            try
            {
                var updatedFolder = await _folderService.Update(id, model, User.Identity.Name);
                return Ok(updatedFolder);
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
                await _folderService.Delete(id, User.Identity.Name);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
