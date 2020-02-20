using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsShared { get; set; }
        public Guid? NoteTypeId { get; set; }
        public Guid UserId { get; set; }
        public Guid? FolderId { get; set; }
    }
}
