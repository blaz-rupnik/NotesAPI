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
    public interface IFolderService
    {
        Task<IEnumerable<Folder>> GetAll();
        Task<Folder> GetById(Guid id, string principalName);
        Task<Folder> Create(Folder folder);
        Task<Folder> Update(Guid id, Folder folder, string principalName);
        Task Delete(Guid id, string principalName);
    }

    public class FolderService : IFolderService
    {
        private readonly IConfiguration _config;

        public FolderService(IConfiguration config)
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

        public async Task<IEnumerable<Folder>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string query = "SELECT Id, Name, UserId FROM Folders";
                conn.Open();
                var result = await conn.QueryAsync<Folder>(query);
                return result;
            }
        }

        public async Task<Folder> GetById(Guid id, string principalName)
        {
            using (IDbConnection conn = Connection)
            {
                string query = "SELECT Id, Name, UserId FROM Folders WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Folder>(query, new { ID = id });
                var folder = result.FirstOrDefault();

                Guid.TryParse(principalName, out Guid userId);

                if (userId == folder.UserId)
                    return folder;
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        public async Task<Folder> Create(Folder folder)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "INSERT INTO Folders (Id, Name, UserId) values (" +
                    "@IdParam, @NameParam, @UserIdParam)";

                folder.Id = Guid.NewGuid();
                await conn.QueryAsync<Folder>(query, new
                {
                    IdParam = folder.Id,
                    NameParam = folder.Name,
                    UserIdParam = folder.UserId
                });

                return folder;
            }
        }

        public async Task<Folder> Update(Guid id, Folder folder, string principalName)
        {
            var dbFolder = await GetById(id, principalName);

            if (dbFolder == null)
                throw new DomainException("Folder not found.");

            //update changes
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string query = "UPDATE Folders SET Name = @NameParam WHERE Id = @IdParam";
                await conn.QueryAsync<Folder>(query, new
                {
                    IdParam = dbFolder.Id,
                    NameParam = !String.IsNullOrEmpty(folder.Name) ? folder.Name : dbFolder.Name
                });

                folder.Id = dbFolder.Id;
                return folder;
            }
        }

        public async Task Delete(Guid id, string principalName)
        {
            var dbFolder = await GetById(id, principalName);

            if (dbFolder == null)
                throw new DomainException("Folder not found.");

            using (IDbConnection conn = Connection)
            {
                conn.Open();

                //delete notes that are inside this folder
                string noteQuery = "DELETE FROM Notes WHERE FolderId = @FolderIdParam";
                await conn.QueryAsync<Folder>(noteQuery, new
                {
                    FolderIdParam = id
                });

                //delete folder
                string query = "DELETE FROM Folders WHERE Id = @IdParam";
                await conn.QueryAsync<Note>(query, new
                {
                    IdParam = id
                });
            }
        }
    }
}
