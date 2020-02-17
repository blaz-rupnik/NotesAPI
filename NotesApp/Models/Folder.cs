using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    public class Folder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Note> Notes { get; set; }
        public Folder()
        {
            Notes = new Collection<Note>();
        }
    }
}
