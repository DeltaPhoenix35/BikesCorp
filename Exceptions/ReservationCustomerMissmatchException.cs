using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Exceptions
{
    public class ReservationCustomerMissmatchException : InvalidOperationException
    {
        public ReservationCustomerMissmatchException()
        {

        }
        public ReservationCustomerMissmatchException(string message) : base(message)
        {

        }
        public ReservationCustomerMissmatchException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
