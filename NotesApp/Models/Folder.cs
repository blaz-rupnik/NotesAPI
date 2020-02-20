using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    public class Folder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }      
    }
}
