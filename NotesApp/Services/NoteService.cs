using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NotesApp.Controllers.Resources;
using NotesApp.Helpers;
using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAll(NoteQuery query, bool isAuthenticated, string principalName);
        Task<Note> GetById(Guid id, bool isAuthenticated, string principalName);
        Task<Note> Create(Note note, string principalName);
        Task<Note> Update(Guid id, Note note, string principalName);
        Task Delete(Guid id, string principalName);
    }
    public class NoteService : INoteService
    {
        private readonly IConfiguration _config;
        private readonly IFolderService _folderService;

        public NoteService(IConfiguration config, IFolderService folderService)
        {
            _config = config;
            _folderService = folderService;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("Default"));
            }
        }

        public async Task<IEnumerable<Note>> GetAll(NoteQuery query, bool isAuthenticated, string principalName)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                string queryDb = "SELECT Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId FROM Notes";

                if (isAuthenticated)
                {
                    queryDb += " WHERE (IsShared = 1 OR UserId = @UserIdParam)";
                }else
                {
                    queryDb += " WHERE IsShared = 1";
                }

                //filtering
                if(query.IsShared != null)
                    queryDb += QueryBuildExtensions.AppendFilter("IsShared", false, false);

                if(query.FolderId != null)
                    queryDb += QueryBuildExtensions.AppendFilter("FolderId", false, false);

                if (!String.IsNullOrEmpty(query.Content))
                    queryDb += QueryBuildExtensions.AppendFilter("Content", true, false);

                //sorting
                bool isSorted = false;
                if(!String.IsNullOrEmpty(query.SortBy) && (query.SortBy.ToLower() == "isshared" || 
                    query.SortBy.ToLower() == "name"))
                {
                    queryDb += QueryBuildExtensions.AppendSort(query.SortBy, query.IsSortAscending);
                    isSorted = true;
                }

                //paging
                if(query.Page != null && query.PageSize != null)
                {
                    queryDb += QueryBuildExtensions.AppendPaging(isSorted, "Name");
                }

                var result = await conn.QueryAsync<Note>(queryDb, new { 
                    PageSizeParam = query.PageSize,
                    PageParam = query.Page,
                    FolderIdParam = query.FolderId,
                    IsSharedParam = query.IsShared,
                    ContentParam = query.Content,
                    UserIdParam = !String.IsNullOrEmpty(principalName) ? Guid.Parse(principalName) : Guid.Empty
                });
                return result;
            }
        }

        public async Task<Note> GetById(Guid id, bool isAuthenticated, string principalName)
        {
            using (IDbConnection conn = Connection)
            {
                string query = "SELECT Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId" +
                    " FROM Notes WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Note>(query, new { ID = id });
                var note = result.FirstOrDefault();

                if (note == null)
                    return null;

                Guid.TryParse(principalName, out Guid userId);

                if (note.IsShared || userId == note.UserId)
                    return note;
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }          
        }

        public async Task<Note> Create(Note note, string principalName)
        {
            //if creating inside folder, check that folder belongs to the user
            if(note.FolderId != null)
            {
                //fails if folder does not belong to user
                var folder = _folderService.GetById((Guid)note.FolderId, principalName);
            }

            if (note.UserId != Guid.Parse(principalName))
                throw new UnauthorizedAccessException();

            using (IDbConnection conn = Connection)
            {
                conn.Open();             
                string query = "INSERT INTO Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) values (" +
                    "@IdParam, @NameParam, @ContentParam, @FolderIdParam, @IsSharedParam, @NoteTypeIdParam, @UserIdParam)";

                note.Id = Guid.NewGuid();
                await conn.QueryAsync<Note>(query, new
                {
                    IdParam = note.Id,
                    NameParam = note.Name,
                    ContentParam = note.Content,
                    FolderIdParam = note.FolderId,
                    IsSharedParam = note.IsShared,
                    NoteTypeIdParam = note.NoteTypeId ?? CodeListConstants.NoteType_RegularText,
                    UserIdParam = note.UserId
                });

                return note;
            }           
        }

        public async Task<Note> Update(Guid id, Note note, string principalName)
        {
            var dbNote = await GetById(id, true, principalName);

            if (dbNote == null)
                throw new DomainException("Note not found.");

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "UPDATE Notes SET Name = @NameParam, Content = @ContentParam, FolderId = @FolderIdParam," +
                    "IsShared = @IsSharedParam, NoteTypeId = @NoteTypeIdParam WHERE Id = @IdParam";
                await conn.QueryAsync<Note>(query, new
                {
                    IdParam = dbNote.Id,
                    NameParam = note.Name,
                    ContentParam = note.Content,
                    FolderIdParam = note.FolderId ?? dbNote.FolderId,
                    IsSharedParam = note.IsShared,
                    NoteTypeIdParam = note.NoteTypeId ?? dbNote.NoteTypeId,
                });

                note.Id = dbNote.Id;
                return note;
            }                
        }

        public async Task Delete(Guid id, string principalName)
        {
            //getbyid also check security
            var dbNote = await GetById(id, true, principalName);

            if (dbNote == null)
                throw new DomainException("Note not found.");

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "DELETE FROM Notes WHERE Id = @IdParam";
                await conn.QueryAsync<Note>(query, new
                {
                    IdParam = id
                });
            }
        }
    }
}
