using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Controllers.QueryParams
{
    public class FolderQuery
    {
        //filtering
        public string Name { get; set; }

        //sorting    
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

        //paging
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
