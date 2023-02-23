using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class ReservationDoesntExistException : InvalidOperationException
    {
        public ReservationDoesntExistException()
        {

        }
        public ReservationDoesntExistException(string message) : base(message)
        {

        }
        public ReservationDoesntExistException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
