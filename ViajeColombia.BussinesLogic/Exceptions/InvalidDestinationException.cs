using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.BussinesLogic.Exceptions
{
    public class InvalidDestinationException : Exception
    {
        public InvalidDestinationException() : base() { }

        public InvalidDestinationException(string? message) : base(message) { }

        public InvalidDestinationException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
