using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Controllers.Resources
{
    public class FolderResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public ICollection<NoteResource> Notes { get; set; }
        public FolderResource()
        {
            Notes = new Collection<NoteResource>();
        }
    }
}
