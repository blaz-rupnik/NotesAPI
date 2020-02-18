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
        Note GetById(Guid id);
        Note Create(Note note);
        void Update(Note note);
        void Delete(Guid id);
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

        public Note GetById(Guid id)
        {
            return _context.Notes.Find(id);
        }

        public Note Create(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

            return note;
        }

        public void Update(Note note)
        {
            var dbNote = _context.Notes.Find(note.Id);

            if (note == null)
                throw new DomainException("Note not found.");

            //update changes
            dbNote.Content = note.Content;

            _context.Notes.Update(note);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var note = _context.Notes.Find(id);
            if(note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }
    }
}
