using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class InvalidDateException : InvalidOperationException
    {
        public InvalidDateException()
        {

        }
        public InvalidDateException(string message) : base(message)
        {

        }
        public InvalidDateException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}