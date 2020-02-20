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
        public IActionResult GetAll()
        {
            var folder = _folderService.GetAll();
            var model = _mapper.Map<FolderResource>(folder);

            return Ok(model);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetById(Guid id)
        //{
        //    try
        //    {
        //        var folder = _folderService.GetById(id);
        //        var model = _mapper.Map<Folder, FolderResource>(folder);

        //        return Ok(model);
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpPost]
        public IActionResult Create([FromBody] FolderResource model)
        {
            var folder = _folderService.Create(_mapper.Map<FolderResource, Folder>(model));
            var result = _mapper.Map<Folder, FolderResource>(folder);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] FolderResource model)
        {
            try
            {
                //var updatedFolder = _folderService.Update(id, _mapper.Map<FolderResource, Folder>(model), User.Identity.Name);
                //var result = _mapper.Map<Folder, FolderResource>(updatedFolder);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    try
        //    {
        //        _folderService.Delete(id, User.Identity.Name);
        //        return Ok();
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return Unauthorized();
        //    }
        //}
    }
}
