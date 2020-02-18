using NotesApp.Helpers;
using NotesApp.Models;
using NotesApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Create(User user, string password);
        IEnumerable<User> GetAll();
        User GetById(Guid id);


    }

    public class UserService : IUserService
    {
        private NotesDbContext _context;

        public UserService(NotesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User Authenticate(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            //success
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new DomainException("Password is invalid.");

            //check if already user with same username
            if (_context.Users.Any(x => x.Username == user.Username))
                throw new DomainException("Username already taken.");

            (user.PasswordHash, user.PasswordSalt) = HashPassword(password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
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
