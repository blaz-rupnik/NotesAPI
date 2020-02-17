using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Folder> Folders { get; set; }
        public User()
        {
            Folders = new Collection<Folder>();
        }
    }
}
