using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotesApp.Controllers.QueryParams;
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
    public class FoldersController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly ILogger<FoldersController> _logger;

        public FoldersController(IFolderService folderService, ILogger<FoldersController> logger)
        {
            this._folderService = folderService;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FolderQuery query)
        {
            try
            {
                var folders = await _folderService.GetAll(query);
                _logger.LogInformation("Folders successfully retrieved");
                return Ok(folders);

            }catch (Exception ex)
            {
                _logger.LogError($"Error retrieving all users. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var folder = await _folderService.GetById(id, User.Identity.Name);

                if (folder == null)
                {
                    _logger.LogError($"Folder with id {id} not found");
                    return NotFound();
                }

                _logger.LogInformation($"Folder {folder.Name} successfully retrieved");
                return Ok(folder);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving folder. Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Folder model)
        {
            try
            {
                var folder = await _folderService.Create(model, User.Identity.Name);
                _logger.LogInformation($"Folder {folder.Name} successfully created");
                return Ok(folder);
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogError("User cannot create folder for another user.");
                return Unauthorized("User cannot create folder for another user.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Folder model)
        {
            try
            {
                var updatedFolder = await _folderService.Update(id, model, User.Identity.Name);
                _logger.LogInformation($"Folder {updatedFolder.Name} successfully updated");
                return Ok(updatedFolder);
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogError("User cannot update folder for another user.");
                return Unauthorized("User cannot update folder for another user.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _folderService.Delete(id, User.Identity.Name);

                _logger.LogInformation("Folder successfully deleted.");
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogError("User cannot delete folder for another user");
                return Unauthorized("User cannote delete folder for another user");
            }
            catch (DomainException ex)
            {
                _logger.LogError("Folder not found");
                return NotFound(ex);
            }
        }
    }
}
