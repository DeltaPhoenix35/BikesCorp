using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class InvalidExpectedReturnDateException : InvalidOperationException
    {
        public InvalidExpectedReturnDateException()
        {

        }
        public InvalidExpectedReturnDateException(string message) : base(message)
        {

        }
        public InvalidExpectedReturnDateException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}