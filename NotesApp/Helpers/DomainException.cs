using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Helpers
{
    /// <summary>
    /// Custom exception, used on business logic
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() : base() { 
        
        }

        public DomainException(string msg) : base(msg)
        {

        }

        public DomainException(string msg, params string[] args)
            : base(String.Format(msg, args))
        {

        }
    }
}
