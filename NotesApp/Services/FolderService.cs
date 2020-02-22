using Dapper;
using Microsoft.Extensions.Configuration;
using NotesApp.Controllers.QueryParams;
using NotesApp.Helpers;
using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface IFolderService
    {
        Task<IEnumerable<Folder>> GetAll(FolderQuery query, string principalName);
        Task<Folder> GetById(Guid id, string principalName);
        Task<Folder> Create(Folder folder, string principalName);
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

        public async Task<IEnumerable<Folder>> GetAll(FolderQuery query, string principalName)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                string dbQuery = "SELECT Id, Name, UserId FROM Folders WHERE UserId = @UserIdParam";

                //filtering
                if (query.Name != null)
                    dbQuery += QueryBuildExtensions.AppendFilter("Name", true, false);

                //sorting
                var isSorted = false;
                if(!String.IsNullOrEmpty(query.SortBy) && query.SortBy.ToLower() == "name")
                {
                    dbQuery += QueryBuildExtensions.AppendSort(query.SortBy, query.IsSortAscending);
                    isSorted = true;
                }

                //paging
                if(query.Page != null && query.PageSize != null)
                {
                    dbQuery += QueryBuildExtensions.AppendPaging(isSorted, "Name");
                }

                
                var result = await conn.QueryAsync<Folder>(dbQuery, new { 
                    NameParam = query.Name,
                    PageParam = query.Page,
                    PageSizeParam = query.PageSize,
                    UserIdParam = Guid.Parse(principalName)
                });
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

                if (folder == null)
                    return null;

                Guid.TryParse(principalName, out Guid userId);

                if (userId == folder.UserId)
                    return folder;
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }

        public async Task<Folder> Create(Folder folder, string principalName)
        {
            //check access
            if (folder.UserId != Guid.Parse(principalName))
                throw new UnauthorizedAccessException();

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
                var tx = conn.BeginTransaction();
                try
                {
                    //delete notes that are inside this folder
                    string noteQuery = "DELETE FROM Notes WHERE FolderId = @FolderIdParam";
                    await conn.QueryAsync<Folder>(noteQuery, new
                    {
                        FolderIdParam = id
                    }, tx);

                    //delete folder
                    string query = "DELETE FROM Folders WHERE Id = @IdParam";
                    await conn.QueryAsync<Note>(query, new
                    {
                        IdParam = id
                    }, tx);

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
