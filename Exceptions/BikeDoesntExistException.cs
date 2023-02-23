using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class BikeDoesntExistException : InvalidOperationException
    {
        public BikeDoesntExistException()
        {

        }
        public BikeDoesntExistException(string message) : base(message)
        {

        }
        public BikeDoesntExistException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
