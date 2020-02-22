using System;
using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models
{
    public class Folder
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid UserId { get; set; }      
    }
}
