﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Models
{
    public class NoteType
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string TypeName { get; set; }
    }
}