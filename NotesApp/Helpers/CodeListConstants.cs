using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Helpers
{
    public class CodeListConstants
    {
        /// <summary>
        /// Regular text type codelist
        /// </summary>
        public static readonly Guid NoteType_RegularText = new Guid("27A6378A-D5F1-437A-9324-FB42E7A0E1EF");
        /// <summary>
        /// Itemized text type codelist
        /// </summary>
        public static readonly Guid NoteType_ItemizedText = new Guid("05514712-1775-47CE-A411-9281F30AE59C");
    }
}
