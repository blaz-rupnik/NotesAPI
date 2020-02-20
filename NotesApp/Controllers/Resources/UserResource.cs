using NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Controllers.Resources
{
    public class UserResource
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
