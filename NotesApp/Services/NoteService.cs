using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        Task<IEnumerable<Note>> GetAll();
        Task<Note> GetById(Guid id, bool isAuthenticate, string principalName);
        Task<Note> Create(Note note);
        Task<Note> Update(Guid id, Note note, string principalName);
        Task Delete(Guid id, string principalName);
    }
    public class NoteService : INoteService
    {
        private readonly IConfiguration _config;

        public NoteService(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("Default"));
            }
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string query = "SELECT Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId FROM Notes";
                conn.Open();
                var result = await conn.QueryAsync<Note>(query);
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

                Guid.TryParse(principalName, out Guid userId);

                if (note.IsShared || userId == note.UserId)
                    return note;
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }          
        }

        public async Task<Note> Create(Note note)
        {
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
