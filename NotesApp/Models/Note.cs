using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    [Table("Notes")]
    public class Note
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsShared { get; set; }

        public NoteType NoteType { get; set; }
        [Required]
        public Guid NoteTypeId { get; set; }

        public User User { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public Folder Folder { get; set; }
        public Guid? FolderId { get; set; }
    }
}
