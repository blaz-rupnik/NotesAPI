using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    [Table("Folders")]
    public class Folder
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        public User User { get; set; }
        [Required]
        public Guid UserId { get; set; }      
        public ICollection<Note> Notes { get; set; }
        public Folder()
        {
            Notes = new Collection<Note>();
        }
    }
}
