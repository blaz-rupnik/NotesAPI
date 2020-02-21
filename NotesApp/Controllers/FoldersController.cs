using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Controllers.QueryParams;
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
        private readonly IFolderService _folderService;

        public FoldersController(IFolderService folderService)
        {
            this._folderService = folderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FolderQuery query)
        {
            var folders = await _folderService.GetAll(query);
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
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
