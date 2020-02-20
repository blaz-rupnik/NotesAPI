using NotesApp.Helpers;
using NotesApp.Models;
using NotesApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Services
{
    public interface INoteService
    {
        IEnumerable<Note> GetAll();
        Note GetById(Guid id, bool isAuthenticate, string principalName);
        Note Create(Note note);
        Note Update(Guid id, Note note, string principalName);
        void Delete(Guid id, string principalName);
    }
    public class NoteService : INoteService
    {
        private NotesDbContext _context;

        public NoteService(NotesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Note> GetAll()
        {
            return _context.Notes;
        }

        public Note GetById(Guid id, bool isAuthenticated, string principalName)
        {
            var note = _context.Notes.Find(id);

            Guid.TryParse(principalName, out Guid userId);

            if (note.IsShared || userId == note.UserId)
                return note;
            else
            {
                throw new UnauthorizedAccessException();
            }
        }

        public Note Create(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

            return note;
        }

        public Note Update(Guid id, Note note, string principalName)
        {
            var dbNote = _context.Notes.Find(id);

            if (dbNote == null)
                throw new DomainException("Note not found.");

            CheckIfModifyingOwnNote(dbNote.UserId, principalName);

            //update changes
            dbNote.Content = note.Content;
            dbNote.IsShared = note.IsShared;
            dbNote.Name = note.Name;
            dbNote.NoteTypeId = note.NoteTypeId;

            _context.Notes.Update(dbNote);
            _context.SaveChanges();

            return dbNote;
        }

        public void Delete(Guid id, string principalName)
        {
            var note = _context.Notes.Find(id);
          
            if (note != null)
            {
                //security
                CheckIfModifyingOwnNote(note.UserId, principalName);

                _context.Notes.Remove(note);
                _context.SaveChanges();
            }else
            {
                throw new DomainException("Note not found");
            }
        }

        private void CheckIfModifyingOwnNote(Guid noteUserId, string principalName)
        {
            if(!Guid.TryParse(principalName, out Guid userId) && noteUserId != userId)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
