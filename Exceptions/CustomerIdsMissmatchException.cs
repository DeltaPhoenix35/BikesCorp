using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class CustomerIdsMissmatchException : InvalidOperationException
    {
        public CustomerIdsMissmatchException()
        {

        }
        public CustomerIdsMissmatchException(string message) : base(message)
        {

        }
        public CustomerIdsMissmatchException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
