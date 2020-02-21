using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Controllers.Resources
{
    public class NoteQuery
    {
        //filters
        public Guid? FolderId { get; set; }
        public bool? IsShared { get; set; }
        public string Content { get; set; }

        //sorting    
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

        //paging
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
