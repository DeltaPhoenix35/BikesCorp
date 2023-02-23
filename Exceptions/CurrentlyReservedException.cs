using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class CurrentlyReservedException : InvalidOperationException
    {
        public CurrentlyReservedException()
        {

        }
        public CurrentlyReservedException(string message) : base(message)
        {

        }
        public CurrentlyReservedException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}