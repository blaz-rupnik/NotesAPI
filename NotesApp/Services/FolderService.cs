using NotesApp.Helpers;
using NotesApp.Models;
using NotesApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface IFolderService
    {
        IEnumerable<Folder> GetAll();
        Folder GetById(Guid id, string principalName);
        Folder Create(Folder folder);
        //void Update(Guid id, Folder folder, string principalName);
        //void Delete(Guid id, string principalName);
    }

    public class FolderService : IFolderService
    {
        private NotesDbContext _context;

        public FolderService(NotesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Folder> GetAll()
        {
            return _context.Folders;
        }

        public Folder GetById(Guid id, string principalName)
        {
            return _context.Folders.Find(id);
        }

        public Folder Create(Folder folder)
        {
            _context.Folders.Add(folder);
            _context.SaveChanges();

            return folder;
        }

        public void Update(Folder folder)
        {
            var dbFolder = _context.Folders.Find(folder.Id);

            if (dbFolder == null)
                throw new DomainException("Folder not found");

            //update changes
            dbFolder.Name = folder.Name;

            _context.Folders.Update(dbFolder);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var folder = _context.Folders.Find(id);

            if(folder != null)
            {
                //TODO: Also remove notes inside folder!
                _context.Folders.Remove(folder);
                _context.SaveChanges();
            }
        }
    }
}
