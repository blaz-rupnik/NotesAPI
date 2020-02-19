using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Controllers.Resources
{
    public class NoteResource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid NoteTypeId { get; set; }
        public Guid? FolderId { get; set; }
        public Guid UserId { get; set; }
    }
}
