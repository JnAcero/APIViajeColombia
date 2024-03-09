using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajeColombia.BussinesLogic.Exceptions
{
    public class RouteDoesntExistException : Exception
    {
        public RouteDoesntExistException(): base() { }
        public RouteDoesntExistException(string? message) : base(message) { }

        public RouteDoesntExistException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
