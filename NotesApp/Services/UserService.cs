using Dapper;
using System.Data.SqlClient;
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
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
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

        public async Task<IEnumerable<User>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string query = "SELECT Id, PasswordHash, PasswordSalt, Username FROM Users";
                conn.Open();
                var result = await conn.QueryAsync<User>(query);
                return result;
            }
        }

        public async Task<User> GetById(Guid id)
        {
            using(IDbConnection conn = Connection)
            {
                string query = "SELECT Id, PasswordHash, PasswordSalt, Username FROM Users WHERE Id = @ID";
                conn.Open();
                var result = await conn.QueryAsync<User>(query, new { ID = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return null;

            using (IDbConnection conn = Connection)
            {
                string query = "SELECT Id, PasswordHash, PasswordSalt, Username FROM Users WHERE Username = @UsernameParam";
                conn.Open();
                var result = await conn.QueryAsync<User>(query, new { UsernameParam = username });
                var user = result.FirstOrDefault();

                if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                    return null;

                //success
                return user;
            }   
        }

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new DomainException("Password is missing.");

            using (IDbConnection conn = Connection)
            {
                conn.Open();
                //check if already user with same username
                string usernameQuery = "SELECT * FROM Users WHERE Username = @UsernameParam";
                var usernameResult = await conn.QueryAsync<User>(usernameQuery, new { @UsernameParam = user.Username });
                if(usernameResult.Any())
                    throw new DomainException("Username already taken.");

                //calculate password
                (user.PasswordHash, user.PasswordSalt) = HashPassword(password);
                
                //generate id and save user to db
                user.Id = Guid.NewGuid();
                string query = "INSERT INTO Users(Id, PasswordHash, PasswordSalt, Username) values (@IdParam,@HashParam,@SaltParam,@UsernameParam)";
                
                await conn.QueryAsync<User>(query, new { 
                    IdParam = user.Id,
                    HashParam = user.PasswordHash,
                    SaltParam = user.PasswordSalt,
                    UsernameParam = user.Username });
                return user;
            }
        }

        private static (byte[], byte[]) HashPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return (passwordHash, passwordSalt);
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i]) return false;
                }
            }
            return true;
        }
    }
}
